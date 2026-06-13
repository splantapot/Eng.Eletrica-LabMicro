/*
 * intimer.h
 *
 *  Created on: 18 de mai de 2026
 *      Author: João Victor Carvalho
 */

#ifndef INTIMER_H_
#define INTIMER_H_

#include <msp430.h>
#include <msp430g2553.h>

#define MOD_TACCR0 (1000-1)             // Defines Counter Module as 1000 (to timer in ms)
extern volatile unsigned long intimer;  // Global timer

void setup_timer(void);                 // Setup function
void reset_timer(void);                 // Reset function

#endif /* INTIMER_H_ */
