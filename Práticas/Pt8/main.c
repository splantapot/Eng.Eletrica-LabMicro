#include "p18f4550.h"
#include "i2c.h"
#include "intimer.h"
#include "acctimer.h"

#define ADDRESS 0x20

#define LR LATDbits.LATD7
#define LED LATDbits.LATD6

#define DELAY 300

void WriteAddr(unsigned char address, unsigned char value);
unsigned char ReadAddr(unsigned char address);

void main(void) {
	signed char status = 0;
	unsigned char count = 0, valor;
	TRISD = 0;
	TRISB = 0x03; // 0000 0011
	ADCON1 = 0xFF;	// Habilita B como digital

	// Timers
	setup_timer();
	setup_acctimer();
	
	// Configurações do I2C
	SSPADD = 49;
	OpenI2C(MASTER, SLEW_OFF);
	LED = 0;

	while (1) {
		LR = (timer%1000)>=500;
		
		if (acctimer >= DELAY) {
			count++;
			count%=32; //Contador de módulo 32, 2**5 (5 bits)
			acctimer = 0;
		}

		WriteAddr(ADDRESS, count & 0x1F); 			// 0001 1111: Escreve o Contador e coloca os bits [5:7] como pull-down
		valor = ((ReadAddr(ADDRESS) >> 5) << 5); 		// Lê a entrada, por exemplo: 010x xxxx (x são os bits dedicados ao contador, por isso iremos fazer o shift)
		// O shift + unshift apaga os bits [0:4]
		LATB |= valor; 	//Liga os leds que estão nas posições RB[5:7] e estão ligados por botões no PCF8574
		LATB &= valor + 0x1F; //Apaga os leds nessas posições, sem mexer nos demais bits
	}
}

/* Write and Read address functions */
void WriteAddr(unsigned char address, unsigned char value){
	IdleI2C();
	StartI2C();
	IdleI2C();
	if (WriteI2C(address << 1) == 0) {
		if (WriteI2C(value) == 0) {
			IdleI2C();
		}
	}
	StopI2C();
	IdleI2C();
}

unsigned char ReadAddr(unsigned char address){
	unsigned char valor;
	IdleI2C();
	StartI2C();
	IdleI2C();
	if (WriteI2C((address << 1)+1) == 0) {
		valor = ReadI2C();
	}
	StopI2C();
	IdleI2C();
	return valor;
}