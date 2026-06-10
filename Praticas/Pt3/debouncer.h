#ifndef DEBOUNCER
#define DEBOUNCER

#include "p18f4550.h"

#define DEBOUNCE_DELAY 50

// Union para números de 8 bits. Pode manipular bits individualmente
typedef union {
	unsigned char value;
	struct {
		unsigned int b0 : 1;
		unsigned int b1 : 1;
		unsigned int b2 : 1;
		unsigned int b3 : 1;
		unsigned int b4 : 1;
		unsigned int b5 : 1;
		unsigned int b6 : 1;
		unsigned int b7 : 1;
	} bits;
} uint8;

void debounce(uint8* condicoes, unsigned long* main_timer);

#endif