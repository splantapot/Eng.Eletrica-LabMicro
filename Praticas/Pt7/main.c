#include "p18f4550.h"
#include "intimer.h"
#include "acctimer.h" // Para uso como contador de 1 seg
#include "spi.h"

#define LR LATDbits.LATD7
#define LOAD LATDbits.LATD6

#define DELAY 200 // Tempo de contagem em ms

void main() {
	unsigned char seg = 0;
	TRISD = 0;
	TRISB = 0;
	
	// Configura os Timers usados
	setup_timer();
	setup_acctimer();
	
	// Configura a comunicação SPI
	OpenSPI(SPI_FOSC_64, MODE_00, SMPMID);
	LOAD = 0;

	while(1) {
		LR = (timer%1000)>=500;

		if (acctimer >= DELAY) {
			seg++;
			seg%=64;	// Contador de Mod 64 (2**6 bits)
			WriteSPI(seg);
			LOAD = 1;	// Aciona o RCLK
			LOAD = 0;
			acctimer=0;
		}
	}
}