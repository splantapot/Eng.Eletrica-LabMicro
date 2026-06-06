#include "p18f4550.h"
#include "intimer.h"
#include "debouncer.h"

#pragma config WDT = OFF
#pragma config PBADEN = OFF

/* ------------------------- Configurações -------------------------- */
#define LR LATDbits.LATD7			// LED Heart

// Pinos de botões
#define BTN1 PORTBbits.RB7
#define BTN2 PORTBbits.RB6
#define BTN3 PORTBbits.RB5
// Condições associadas
#define btn2 botoes.bits.b0
#define btn3 botoes.bits.b1
#define btn2and3 botoes.bits.b2

/* ------------------------------- Main ----------------------------- */

void main(void) {
	uint8 botoes = 0;
	unsigned char blink_3 = 0;
	
	TRISD = 0;
	TRISB = 0xE0;
	
	ADCON1 = 0x0F;					// Torna Pinos RB0:4 Digital Inputs
	INTCON2bits.RBPU = 1;			// Desabilita os resitores PULLUP de B
	
	setup_timer();					// Inicia "timer"
	
	LATDbits.LATD5 = 0;
	
	while(1) {
		LR = (timer%1000) >= 500;
		
		LATDbits.LATD6 = BTN1;
		
		btn2 = BTN2;
		btn3 = BTN3;
		btn2and3 = BTN2 && BTN3;
		
		debounce(&botoes, &timer);
		
		if (btn2and3) {
			blink_3 = !blink_3;
		} else if(btn2) {
			LATDbits.LATD5 = !LATDbits.LATD5;
		} else if (btn3) {
			LATDbits.LATD4 = !LATDbits.LATD4;
		}
		
		if (blink_3) {
			LATDbits.LATD4 = (timer%100) >= 50;
		}
	}
}