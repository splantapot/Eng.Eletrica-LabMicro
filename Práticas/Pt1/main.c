#include "p18f4550.h"
#include "intimer.h"

#pragma config WDT = OFF
#pragma config PBADEN = OFF

#define DEBOUNCE_DELAY 50
#define LR LATDbits.LATD7		// LED Heart

#define BTN1 PORTBbits.RB7
#define BTN2 PORTBbits.RB6
#define BTN3 PORTBbits.RB5

void main(void) {
	unsigned long timerB2 = 0;		// Timer para debounce de BTN2
	unsigned char statusB2 = 0;		// Status registrado botão BTN2
	unsigned char pressedB2 = 0;	// Indica se BTN2 foi pressionado ou não
	
	unsigned long timerB3 = 0;		// Timer para debounce de BTN2
	
	TRISD = 0;
	TRISB = 0xE0;
	
	ADCON1 = 0x0F;					// Torna Pinos RB0:4 Digital Inputs
	INTCON2bits.RBPU = 1;			// Desabilita os resitores PULLUP de B
	
	setup_timer();
	
	LATDbits.LATD5 = 0;
	
	while(1) {
		LR = (timer%1000) >= 500;
		
		LATDbits.LATD6 = BTN1;
		
		if (BTN2!=statusB2) {
			statusB2 = BTN2;
			pressedB2 = 0;
			timerB2 = timer;
		}
		
		if (timer-timerB2>=DEBOUNCE_DELAY && (BTN2==statusB2)) {
			if (!pressedB2 && BTN2) {
				pressedB2 = 1;
				LATDbits.LATD5 = !LATDbits.LATD5;
			}	
		}
	}
}