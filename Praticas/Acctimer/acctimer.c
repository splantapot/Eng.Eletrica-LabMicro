/* ----------- Interrupção para tempo mais preciso -------------- */
#include "acctimer.h"

volatile unsigned long acctimer = 0;

#pragma code high_vector=0x08
void high_interrupt(void) {
	_asm GOTO AccTimer _endasm
}
#pragma code

#pragma interrupt AccTimer
void AccTimer(void) {
	if (TMR1F) {
		TMR1F = 0;
		// Limpeza manual para otimizar o custo de usar "reset_timer"
		TMR1H = INIT_TMR1_H; 
		TMR1L = INIT_TMR1_L;
		acctimer++;
	}
}

void setup_acctimer(void) {
	RCONbits.IPEN = 1;			// Habilita interrupções com prioridade
	INTCONbits.GIE = 1;			// Habilita
	INTCONbits.GIEH = 1;		// Habilita interrupções de alta prioridade
	
	PIE1bits.TMR1IE = 1;		// Habilita interrupções do TMR1
	TMR1F = 0;			// Limpa a flag de TMR1
	IPR1bits.TMR1IP = 1;		// Coloca interrupções de TMR1 como alta prioridade
	
	reset_acctimer();
	
	OpenTimer1(
		TIMER_INT_ON &
		T1_16BIT_RW &
		T1_SOURCE_INT &
		T1_PS_1_1 &
		T1_OSC1EN_OFF
	);
}

void reset_acctimer(void) {
	// Retorna os valores do TIMER para os valores iniciais.
	TMR1H = INIT_TMR1_H; 
	TMR1L = INIT_TMR1_L;
}