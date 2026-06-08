#include "p18f4550.h"
#include "acctimer.h"

#define LR LATDbits.LATD7

void main(void) {
	unsigned int i = 0, c = 0;
	TRISD = 0;
	setup_acctimer();
	while(1) {
		if (acctimer >= 1000) {
			c++;
			acctimer = 0;
			reset_acctimer();
			for (i = 0; i < c; ){
				LR = (acctimer%200)>=100;
				if (acctimer>=200){
					acctimer = 0;
					reset_acctimer();
					i++;	
				}		
			}
			LR = 0;
			c%=10;
		}
	}
}