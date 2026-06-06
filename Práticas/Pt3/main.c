#include "p18f4550.h"
#include "timers.h"
#include "intimer.h"
#include "debouncer.h"

#pragma config PBADEN = OFF
#pragma config WDT = OFF

#define LR LATDbits.LATD7
#define H1 LATDbits.LATD6			// Habilita o dígito 1 do display (esquerda)
#define H2 LATDbits.LATD5			// Habilita o dígito 2 do display (direita)
#define DISPLAY PORTB				// Porta dedicada ao display

#define BTNMenos PORTDbits.RD3
#define BTNMais PORTDbits.RD2

#define pMenos botoes.bits.b0			//Apena configura as condicoes de pressionados
#define pMais botoes.bits.b1
#define pAmbos botoes.bits.b2

#define MAX_DELAY 300
#define MIN_DELAY 30
#define INC_DELAY 60

const unsigned char nums[] = {
	0x3F, 0x06, 0x5B, 0x4F, 0x66, 0x6D, 0x7D, 0x07, 0x7F, 0x6F
};

void display_num(unsigned char num, unsigned int delay) {
	if ((timer%delay)>=(delay/2)) {
		H1 = 1;
		H2 = 0;
		DISPLAY = nums[num/10];
	} else {
		H1 = 0;
		H2 = 1;
		DISPLAY = nums[num%10];
	}
}

void main(void) {
	signed int delay = 100;
	unsigned char contador = 0, mode = 0;
	uint8 botoes= {0};
	
	TRISD = 0x0C;
	TRISB = 0;
	setup_timer();
	
	OpenTimer1(
		TIMER_INT_OFF &
		T1_8BIT_RW &
		T1_SOURCE_EXT &
		T1_PS_1_1 &
		T1_OSC1EN_OFF &
		T1_SYNC_EXT_OFF
	);
	
	while (1) {
		LR = (timer%1000)>=200;
		pMenos = BTNMenos;
		pMais = BTNMais;
		pAmbos = BTNMenos && BTNMais;
		debounce(&botoes, &timer);
		if (pAmbos) {
			mode=!mode;
			WriteTimer1(0);
			contador=0;
		} else if (pMenos) {
			if (mode) {
				delay -= INC_DELAY;
				delay = delay < MIN_DELAY? MIN_DELAY : delay;
			} else {
				contador-=contador>0?1:0;
			}
		} else if (pMais){
			if (mode) {
				delay += INC_DELAY;
				delay = delay > MAX_DELAY? MAX_DELAY : delay;
			} else {
				contador+=contador<99?1:0;
			}
		}
		
		if (mode) {
			if (ReadTimer1() > 99) {
				WriteTimer1(0);
			}
			display_num(ReadTimer1(), delay);
		} else {
			display_num(contador, delay);
		}
	}
}