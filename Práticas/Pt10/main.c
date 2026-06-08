#include "p18f4550.h"
#include "acctimer.h"	// O acctimer trabalha com o Timer 1, o "intimer.h" utilizava o Timer 2
#include "pwm.h"
#include "timers.h"
#include "adc.h"

#pragma config CCP2MX = ON // RC1 como pino do CCP2

#define LR LATDbits.LATD7

#define DTmax 600 //Período de Duty Cycle Máximo, dado pelas equações do livro
#define DTp 6 // O que 1% do Duty Cycle representa, em float

void main(void) {
	unsigned int valor = 0, dt = 0; // Variável para controle do Duty Cycle final
	TRISD = 0x00;
	TRISA = 0x01;
	setup_acctimer();
	
	// Setup Timer 2
	OpenTimer2(TIMER_INT_OFF & T2_PS_1_1 & T2_POST_1_1);

	// Configura CCP1 e CCP2 como PWM
	OpenPWM1(149);
	SetDCPWM1(dt);
	
	// Configura o Módulo Conversor ADC
	OpenADC(
	    ADC_FOSC_64 & ADC_RIGHT_JUST & ADC_16_TAD, // Alterado para FOSC_64
	    ADC_CH0 & ADC_INT_OFF & ADC_VREFMINUS_VSS & ADC_VREFPLUS_VDD,
	    ADC_1ANA
	);

	SetChanADC(ADC_CH0);

	while (1) {
		LR = (acctimer%1000)>=500;
		
		ConvertADC();
		while(BusyADC());
		valor = ReadADC();
		dt = (unsigned int)(((unsigned long)DTmax*(unsigned long)valor)/1023);
		SetDCPWM1(dt);
	}
}