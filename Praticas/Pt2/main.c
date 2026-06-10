#include "p18f4550.h"
#include "intimer.h"
#include "debouncer.h"

#pragma config WDT = OFF
#pragma config PBADEN = OFF			// Habilita PORTB para ser usada como I/O

#define LR LATDbits.LATD7

#define LED1 LATDbits.LATD6
#define LED2 LATDbits.LATD5
#define LED3 LATDbits.LATD4
#define SCHEDULE_TIME 1000

const char teclado[4][4] = {
	{'1','2','3','A'},
	{'4','5','6','B'},
	{'7','8','9','C'},
	{'*','0','#','D'},
};

void main(void) {
	unsigned long timers[3] = {0,0,0};			// Timers de espera dos leds
	unsigned char l, c, blink = 0;				// Variáveis pra linha, coluna e blink
	
	uint8 tecla;
	TRISD = 0;
	LATD = 0;
	
	TRISB = 0x0F;					// Coloca bits 7:4 como saída, e 3:0 como entradas
	INTCON2bits.RBPU = 0;			// Habilita PULLUPs internos da porta B
	ADCON1 = 0x0F;					// Configura pinos RB3:0 como entradas digitais
	
	setup_timer();
	
	while(1) {
		for (l=0; l<4; l++) {
			LATB = ~(0b00000001 << (7-l));
			for (c = 0; c<4; c++) {
				if ((~PORTB) & (1<<(3-c))) tecla.value = teclado[l][c];
			}
		}
		
		debounce(&tecla, &timer);
		switch (tecla.value) {
			case '1': LED1 = 1; break;
			case '2': LED2 = 1; break;
			case '3': LED3 = 1; break;
			case 'A': LATD &= 0x8F; break;			//Apaga todos os LEDs, (& 1000 1111)
			case '4': LED1 = !LED1; break;
			case '5': LED2 = !LED2; break;
			case '6': LED3 = !LED3; break;
			case 'B': blink = !blink; break;
			case '7': LED1 = 0; break;
			case '8': LED2 = 0; break;
			case '9': LED3 = 0; break;
			case 'C': LATD ^= 0x70; break; 			// O 0xF0 limita a ação para apenas os LEDS (caso contrário, lotaria a porta D)
			case '*': timers[0] = timer+SCHEDULE_TIME; break;
			case '0': timers[1] = timer+SCHEDULE_TIME; break;
			case '#': timers[2] = timer+SCHEDULE_TIME; break;
			case 'D': LATD <<= 1; break;
		}
		
		LR = (timer%1000)>=500;
		
		if (blink) {
			LED1 = (timer%200)>=100;
			LED2 = !LED1;
			LED3 = LED1;
		} else {
			for (l=0; l<3; l++) {
				if (timers[l]) {
					if (timer<timers[l]) {
						LATD |= (1<<(6-l));			// Liga o bit cujo timer está agendado
					} else {
						timers[l] = 0;
						LATD &= ~(1<<(6-l));		// Desliga bit cujo timer agendado foi excedido
					}
				}
			}
		}
	}
}