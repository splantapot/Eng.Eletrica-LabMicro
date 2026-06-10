#include "p18f4550.h"
#include "stdio.h"
#include "lcd1602.h"
#include "debouncer.h"
#include "intimer.h"

/* ---------------------- Definições ---------------------------- */

#define LR LATDbits.LATD7
#define BTNHora PORTDbits.RD6
#define BTNMin PORTDbits.RD5
#define BTNSeg PORTDbits.RD4
#define pHora botoes.bits.b0
#define pMin botoes.bits.b1
#define pSeg botoes.bits.b2
#define pDuplo botoes.bits.b3

unsigned char h = 0, m = 0, s = 0;
volatile unsigned long acctimer = 0;

/* ----------- Interrupção para tempo mais preciso -------------- */

void AccTimer(void);

#pragma code high_vector=0x08
void high_interrupt(void) {
	_asm GOTO AccTimer _endasm
}
#pragma code

#define TMR1F PIR1bits.TMR1IF // Flag do Tmr1
#define INIT_TMR1 53536

#pragma interrupt AccTimer
void AccTimer(void) {
	if (TMR1F) {
		TMR1F = 0;
		TMR1H = INIT_TMR1 >> 8; 
		TMR1L = INIT_TMR1 & 0xFF;
		acctimer++;
	}
}

void setup_acctimer(void) {
	RCONbits.IPEN = 1;			// Habilita interrupções com prioridade
	INTCONbits.GIE = 1;			// Habilita
	INTCONbits.GIEH = 1;		// Habilita interrupções de alta prioridade
	
	PIE1bits.TMR1IE = 1;		// Habilita interrupções do TMR1
	TMR1F = 0;					// Limpa a flag de TMR1
	IPR1bits.TMR1IP = 1;		// Coloca interrupções de TMR1 como alta prioridade
	
	TMR1H = INIT_TMR1 >> 8; 
	TMR1L = INIT_TMR1 & 0xFF;
	
	OpenTimer1(
		TIMER_INT_ON &
		T1_16BIT_RW &
		T1_SOURCE_INT &
		T1_PS_1_1 &
		T1_OSC1EN_OFF
	);
}
// Test started at: 20:37:20
/* --------------------------- Main ----------------------------- */

void main(void) {
	unsigned char show_delay = 0;
	unsigned long lcdtimer = 0;
	char str[17];
	uint8 botoes;
	TRISD = 0x70;
	TRISB = 0;
	
	setup_timer();
	setup_acctimer();
	LCD_Init();
	
	while(1) {
		LR = (timer%1000)>=500;
		
		pHora = BTNHora;
		pMin = BTNMin;
		pSeg = BTNSeg;
		pDuplo = (BTNHora && BTNMin)||(BTNHora && BTNSeg)||(BTNSeg && BTNMin);
		debounce(&botoes, &timer);
		if (pDuplo) {
			show_delay = !show_delay;
		} else if (pHora) {
			h++;
		} else if (pMin) {
			m++;
		} else if (pSeg) {
			s++;
		}
		if (acctimer>=1000) { acctimer = 0; s++; }		// Limita o timer de 
		if (s >= 60) { s = 0; m++; }					// Limita segundos
		if (m >= 60) { m = 0; h++; }					// Limita minutos
		if (h >= 24) { h = 0; }							// Limita horas
		
		if (timer-lcdtimer >= 500) {
			lcdtimer = timer;
			LCD_Clear();
			sprintf(str, "%02d:%02d:%02d", h, m, s);
			LCD_String_xy(str, 4, 1);
			if (show_delay) {
				sprintf(str,"dt: %lu", timer-lcdtimer);
				LCD_String_xy(str, 4, 2);
			}
			lcdtimer = timer;
		}
		
	}
}