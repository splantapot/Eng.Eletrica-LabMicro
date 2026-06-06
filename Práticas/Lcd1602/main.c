#include "p18f4550.h"
#include "lcd1602.h"
#include "intimer.h"

#pragma config WDT = OFF
#pragma config PBADEN = OFF

#define LR LATDbits.LATD7
#define LCD_DELAY 1000

void main(void) {
	unsigned long lcdt = 0;
	char str[] = "{ Jvcr }";
	TRISD = 0;
	TRISB = 0;
	setup_timer();
	LCD_Init();
	
	while (1) {
		LR = (timer%1000)>=500;
		if (timer-lcdt >= LCD_DELAY) {
			LCD_Clear();
			LCD_String_xy(str, 4, 1);
			LCD_Char_xy('x', 15, 2);
			lcdt = timer;
		}
	}
}
