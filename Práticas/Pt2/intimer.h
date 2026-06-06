#ifndef INTIMER

// Timer por interupção com TIMER2
#define INTIMER
#define MS_INTERVAL 4 // Quantos "ms" uma interrupção representa

#include "p18f4550.h"
#include "timers.h"

#define T2FLAG PIR1bits.TMR2IF

extern volatile unsigned long timer;

void ISR_lowint(void);
void setup_timer(void);

#endif