#ifndef ACCTIMER

// Timer por interupção com TIMER1
// Usado como timer paralelo (Práticas de relógio, temporização)
#define ACCTIMER

#include "p18f4550.h"
#include "timers.h"

#define TMR1F PIR1bits.TMR1IF // Flag do Tmr1
#define INIT_TMR1 53536
// Macros para posições iniciais do TMR1
#define INIT_TMR1_H INIT_TMR1>>8
#define INIT_TMR1_L INIT_TMR1 & 0xFF

extern volatile unsigned long acctimer;

void AccTimer(void);
void setup_acctimer(void);
void reset_acctimer(void);

#endif