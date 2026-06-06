#include "intimer.h"

volatile unsigned long timer = 0;

// Configura vetor de interrupção em 0x18 =======
#pragma code lowint = 0x18
void lowint() {
	_asm GOTO ISR_lowint _endasm
}
#pragma code

// Configura função de lowinterrupt =============
#pragma interruptlow ISR_lowint
void ISR_lowint() {
	if (T2FLAG) {
		T2FLAG = 0;
		timer+=MS_INTERVAL;
	}
}

// Inicialização ================================
void setup_timer() {
	RCONbits.IPEN = 1;		// Habilita interrupções com prioridade
	INTCONbits.GIE = 1;
	INTCONbits.GIEL = 1;	// Habilita interrupções de "Baixa Prioridade"
	INTCONbits.GIEH = 1;    // Habilita interrupções de "Alta Prioridade"

	PIE1bits.TMR2IE = 1; 	// Habilitar interrupção de T2
	IPR1bits.TMR2IP = 0;	// Configura como "Baixa Prioridade"
	PR2 = 249;				// Limita a contagem a 250 valores
	T2FLAG = 0; 			// Limpa a Flag
	
	OpenTimer2(
		TIMER_INT_ON &
		T2_PS_1_16 &
		T2_POST_1_12
	);
}
