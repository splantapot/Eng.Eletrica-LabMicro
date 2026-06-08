#include "p18f4550.h"
#include "acctimer.h"	// O acctimer trabalha com o Timer 1, o "intimer.h" utilizava o Timer 2
#include "pwm.h"
#include "timers.h"

#pragma config CCP2MX = ON // RC1 como pino do CCP2

#define LR LATDbits.LATD7

#define DTmax 300 //Período de Duty Cycle Máximo, dado pelas equações do livro
/*
* O cálculo a seguir define o DTp, que é o valor de 1% do Duty Cycle.
* A expressão comentada é genérica, e usa float para o cálculo com qualquer valor.
* Como no nosso caso temos um período de 10kHz, o DTp fica um inteiro igual a 3.
* Por isso comentamos o cálculo genérico para otimizar os cálculos que o PIC fará.
*/
// #define DTp ((float)((float)DTmax/(float)100))
#define DTp 3

void main(void) {
	unsigned char dt = 0;	// Variável para a % do duty_cycle ativa (Ex: "dt = 100" => 100% )
	unsigned long timer = 0;
	TRISD = 0;
	setup_acctimer();
	
	// Setup Timer 2
	OpenTimer2(TIMER_INT_OFF & T2_PS_1_4 & T2_POST_1_1);

	// Configura CCP1 e CCP2 como PWM
	OpenPWM1(74);		// Valor encontrado no cálculo do 
	OpenPWM2(74);

	SetDCPWM1(DTp*dt);
	SetDCPWM2(DTp*(dt>>1));

	while (1) {
		LR = (acctimer%1000)>=500;
		
		if (acctimer-timer>=1000) {
			dt = dt+10>100? 0 : dt+10;
			SetDCPWM1(DTp*(int)dt);		 //Garante que o tipo de dt não cause overflow
			SetDCPWM2(DTp*(int)(dt>>1));	// Realiza uma divisão por 2 menos custosa que com float
			timer = acctimer;
		}
	}
}