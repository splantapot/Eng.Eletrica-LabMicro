#include "p18f4550.h"
#include "intimer.h"

#pragma config WDT = OFF
#pragma config PBADEN = OFF

#define LR LATDbits.LATD7
#define BTN PORTBbits.RB0
#define LED LATDbits.LATD6

#define FLAG_INT0 INTCONbits.INT0IF
unsigned long timer_int = 0;

void RTI_High(void);

#pragma code high_vector=0x08
void high_interrupt(void) {
	_asm GOTO RTI_High _endasm
}
#pragma code

#pragma interrupt RTI_High
void RTI_High(void) {
	if (FLAG_INT0) {
		// Detectou borda do botão
		FLAG_INT0 = 0;
		timer_int = timer;
	}
}

void main(void) {
	TRISD = 0;
	TRISB = 0xFF;
	
	// Habilita o pino B0
	ADCON1 = 0x0F;
	INTCON2bits.RBPU = 1;
	
	// Habilitar interrupções
	INTCONbits.GIE = 1;
	RCONbits.IPEN = 1;
	INTCONbits.GIEH = 1;
	
	// Habiliar interrupções do pino B0
	INTCONbits.INT0IE = 1;
	FLAG_INT0 = 0;
	INTCON2bits.INTEDG0 = 1;
	
	setup_timer();
	Sleep();
	
	while(1) {
		LR = (timer%1000) >= 500;
		if (timer-timer_int >= 100) {
			LED = !LED;
			Sleep();
		}
	}
}