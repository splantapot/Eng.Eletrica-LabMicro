#include "p18f4550.h"
#include "lcd1602.h"
#include "delays.h"

/****************************Functions********************************/

void LCD_Init(void) {
	//OSCCON=0x72;					/*Use Internal Oscillator with Frequency 8MHZ*/
	delay_ms(15);			/* 15ms, 16x2 LCD Power on delay */
   	LCD_TRIS = 0x00;		/* LCD_TRIS como OUTPUT */
   	RS_TRIS = 0;			/* RS como OUTPUT */
   	EN_TRIS = 0;			/* EN como OUTPUT */
	LCD_Command(0x38);	/*uses 2 line and initialize 5*7 matrix of LCD*/
	LCD_Command(0x01);	/*clear display screen*/
	LCD_Command(0x0c);	/*display on cursor off*/
	LCD_Command(0x06);	/*increment cursor (shift cursor to right)*/
}

void LCD_Clear(void) {
    LCD_Command(0x01);     /*clear display screen*/
}

void LCD_Command(char cmd) {
	LCD_DATA = cmd;		/*Send data to PORT as a command for LCD*/   
	RS = 0;				/*Command Register is selected*/
	EN = 1;				/*High-to-Low pulse on Enable pin to latch data*/
	EN = 0;
	delay_ms(3);	
}

void LCD_Char(char data) {
	LCD_DATA = data;		/*Send data to LCD*/  
	RS = 1;				/*Data Register is selected*/
	EN=1;				/*High-to-Low pulse on Enable pin to latch data*/   
	EN=0;
	delay_ms(1);
}

void LCD_Char_xy(char data, unsigned char x, unsigned char y) {
	char location = 0;
    if (y<=1) {
	    location = (0x80) | ((x) & 0x0F);      /*Print message on 1st row and desired location*/
	    LCD_Command(location);
    } else {
    	location = (0xC0) | ((x) & 0x0F);      /*Print message on 2nd row and desired location*/
    	LCD_Command(location);    
    }
    delay_ms(5);
    LCD_Char(data);
}

void LCD_String(const char *msg) {
	while(*msg) {
		LCD_Char(*msg);
		msg++;
	}
}

void LCD_String_xy(const char* msg, unsigned char x, unsigned char y) {
    char location = 0;
    if (y<=1) {
	    location = (0x80) | ((x) & 0x0F);      /*Print message on 1st row and desired location*/
	    LCD_Command(location);
    } else {
    	location = (0xC0) | ((x) & 0x0F);      /*Print message on 2nd row and desired location*/
    	LCD_Command(location);    
    }
    delay_ms(5);
    LCD_String(msg);
}

/*********************************Delay Function********************************/

/*
 * Gera um atraso seguro em milissegundos usando um laço de repetição.
 * Permite valores muito maiores que 21ms sem transbordar o Timer de 8 bits.
 */
void delay_ms(unsigned int val) {
    unsigned int i;
    for (i = 0; i < val; i++) {
        // CLK_MHZ é 12. Delay1KTCYx(12) gera exatos 12.000 ciclos = 1 ms.
        Delay1KTCYx(CLK_MHZ); 
    }
}