#ifndef LCD1602
#define LCD1602

#include "p18f4550.h"

#define CLK_MHZ 12 					/* Clock após o Fosc, em MHz*/
#define LCD_DATA LATB				/* LCD_DATA -> Pinos D0 a D7 usados para transmissão de dados. */
#define LCD_TRIS TRISB				/* LCD_PORT -> Associa um registrador TRIS à porta que será usada para a transmissão de dados.*/
#define RS LATDbits.LATD0			/* Pino para Register Select (RS)*/
#define RS_TRIS TRISDbits.RD0		/* TRIS associado a RS */
#define EN LATDbits.LATD1			/* Pino para Enable (EN ou E) */
#define EN_TRIS TRISDbits.RD1		/* TRIS associado a EN */

void LCD_Init(void);
void LCD_Clear(void);
void LCD_Command(char cmd);
void LCD_Char(char data);
void LCD_Char_xy(char data, unsigned char x, unsigned char y);
void LCD_String(const char *msg);
void LCD_String_xy(const char* msg, unsigned char x, unsigned char y);
void delay_ms(unsigned int val);

#endif