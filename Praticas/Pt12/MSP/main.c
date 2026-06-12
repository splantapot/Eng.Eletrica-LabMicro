#include <msp430.h> 

#define tp BIT3 // Transistor BC639 de Programacao
#define tu BIT4 // Transistor BC337 de Utilizacaoo

void main(void) {
	WDTCTL = WDTPW | WDTHOLD;	// Stop Watchdog Timer
	
    // Clock 1MHz -> Implementar um método de calibração depois
    DCOCTL = 68;   BCSCTL1 = 135;

    // Configurações para escrita com o BSLDEMO + Transistores
    P1DIR &= ~BIT5;            // Entrada
    P1REN &= ~(BIT5 + BIT2);   // Sem resistor nos bits 5 e 2
    P1REN |= BIT1;             // Habilita PULLUP no pino RX do MSP
    P1DIR |= tp + tu;          // Coloca os pinos dos transistores como saída
    P1REN &= ~tp;              // Desabilita resitor
    P1OUT &= ~tp;              // Limpa o bit do transistor de prog
    P1REN |= tu;               // Habilita o resistor de utilização
    P1OUT |= tu | BIT1;        // Saída nesses bits



	return 0;
}
