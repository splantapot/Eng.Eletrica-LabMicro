#include "p18f4550.h"
#include "timers.h"
#include "intimer.h"

#pragma config WDT = OFF 	//Desabilita o WDT

#define LED PORTDbits.RD7

void main() {
	TRISDbits.RD7 = 0;
	LED = 1;
	setup_timer();

	while(1) {
		LED = (timer%1000) >= 500;
	}
}
