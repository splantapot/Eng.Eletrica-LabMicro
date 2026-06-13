/**
 * main.c
 */

//powershell.exe -ExecutionPolicy Bypass -File "C:\flash-ch340.ps1" "${CG_TOOL_HEX}" "${BuildArtifactFileName}" "${BuildArtifactFileBaseName}"

#include <msp430.h>
#include "intimer.h"

#define RXD BIT1 // P1.1 = UCA0RXD
#define TXD BIT2 // P1.2 = UCA0TXD

#define tp BIT3 //transistor bc639 de programacao
#define tu BIT4 //transistor bc337 de utilizacaoo

#define BYTE_BAUDRATE_OK (0x55) //'U'
#define BYTE_CALIBRACAO_OK (0xAA)

volatile unsigned char fila_vetor[50], i = 0;
volatile unsigned char lin_rx_buffer[12], lin_rx_index = 0;
volatile unsigned char lin_frame_ready = 0;
volatile unsigned char baud_ok = 0, calibrado_ok = 0;

void reset_uart(void);
void escrever_byte_tx(char c);
void escrever_str_tx(char* str);
void escrever_calibracao(void);

void main(void) {
    WDTCTL = WDTPW + WDTHOLD;
    // UCA0RXBUF = 9; lin_rx_buffer[0] = 9; lin_rx_buffer[1] = 9; // Teste

    // Clock 1 MHz
    // DCOCTL = 68;   BCSCTL1 = 135; // Clock original

    // Chaveamento de Transistores
    P1DIR &= ~BIT5;           //entrada
    P1REN &= ~(BIT5 + BIT2);  //sem
    P1REN |= BIT1;            //<<<<<<<<<habilita resistor de pullup no pino RX do msp<<<<<<<<<<<<<
    P1DIR |= tp + tu;         //s
    P1REN &= ~tp;             //sem resistor
    P1OUT &= ~tp;             //n
    P1REN |= tu;              //resistor on
    P1OUT |= tu | BIT1;       ////<<<<<<<<<<<

    // Iniciar UART
    reset_uart();
    __bis_SR_register(GIE);

    // Inicia o INTIMER
    setup_timer();
    unsigned long timer_db = 0; //Tempo para debounce
    unsigned char pressionado = 0;

    // Configurar LEDs
    P2DIR |= BIT4;
    P2REN |= BIT4;
    P2OUT &= ~BIT4;

    P3DIR |= BIT5 | BIT6 | BIT7;
    P3REN |=  BIT5 | BIT6 | BIT7;
    P3OUT &= ~( BIT5 | BIT6 | BIT7);

    // Configurar BTN
    P2DIR &= ~(BIT5);
    P2REN |= BIT5;
    P2OUT &= ~(BIT5);

    while (1) {
        P3OUT = (UCA0ABCTL & UCBTOE)? P3OUT|BIT6 : P3OUT&(~BIT6);
        P3OUT = (UCA0ABCTL & UCSTOE)? P3OUT|BIT5 : P3OUT&(~BIT5);

        // Iremos alterar para a P2.4 quando tivermos mais LEDs
        P3OUT = lin_rx_buffer[1] == 0xAA? P3OUT|BIT7 : P3OUT&(~BIT6);

        // Caso seja uma calibração de DCOCTL e BCSCTL1
        // Se 0xAA está na 4ª posição, então o C# ainda está enviando os 2 bytes de calibração nas posições rx[1] e [2]
        if (!calibrado_ok && lin_rx_buffer[3] == 0xAA) {
            calibrado_ok = 1;
            DCOCTL = lin_rx_buffer[1];
            BCSCTL1 = lin_rx_buffer[2];
            reset_uart();           // Reinicia o UART para funcionar com o novo clock
            escrever_calibracao();  // Envia Byte de confirmação
        }

        // Caso o botão P2.5 seja pressionado (Com debouncer)
        if (P2IN & BIT5) {
            if (!pressionado && intimer-timer_db >= 50) {
                escrever_calibracao();
                pressionado = 1;
            }
        } else {
            timer_db = intimer;
            pressionado = 0;
        }

        // Caso seja o término de uma calibração de baudrate
        if (baud_ok == 1) {
            baud_ok = 2;
            escrever_byte_tx(BYTE_BAUDRATE_OK); // Envia Byte de confirmação
        }

        // Se houve Break Timeout Error ou Synch Timeout Error, solicita recalibração...
        // if ((UCA0ABCTL & UCBTOE) || (UCA0ABCTL & UCSTOE)) {
        //    reset_uart();
        // }
    }
}

// ===================================================================
// Função para reset de UART
// ===================================================================
// Utilizamos para os erros de Break Timeout e Synch Timeout:
// Chama uma recalibração
void reset_uart(void) {
    // Pinos UART
    P1SEL |= RXD + TXD;
    P1SEL2 |= RXD + TXD;
    P1DIR |= TXD;

    // Configurações de UART
    UCA0CTL1 |= UCSWRST;
    UCA0CTL0 = UCMODE_3;                // Modo LIN
    UCA0CTL1 |= UCSSEL_2 | UCBRKIE;     // SMCLK e Habilita Detecção de Break

    // Valores iniciais ( sobrescritos pelo auto-baud )
    UCA0BR0 = 85;       // Valor errado, mas próximo, de propósito. O certo era 80.
    UCA0BR1 = 0x00;
    // UCA0MCTL = UCBRS_1;

    UCA0ABCTL = UCABDEN; // Auto Baud Detection

    UCA0CTL1 &= ~UCSWRST;

    IE2 |= UCA0RXIE; // Interrupção RX

    baud_ok = 0;    // Indica que haverá uma calibração.
}

// ===================================================================
// Interrupção RX
// ===================================================================
#pragma vector=USCIAB0RX_VECTOR
__interrupt void USCI0RX_ISR(void) {
    unsigned char rx_data = UCA0RXBUF; // Le e limpa flag
    if (i<50) fila_vetor[i++] = rx_data;

    if (!baud_ok) {
        if (rx_data == 0xAA) baud_ok = 1;
    } else {
        if (rx_data == 0x55) lin_rx_index = 0;
        if (lin_rx_index < sizeof(lin_rx_buffer)) lin_rx_buffer[lin_rx_index++] = rx_data;
    }

}

// ===================================================================
// Funções para TX
// ===================================================================
void escrever_byte_tx(char c) {
    while (!(IFG2 & UCA0TXIFG));   // Aguarda o tx buffer
    UCA0TXBUF = c;                 // Envia um char
}

void escrever_str_tx(char* str) {
    while (*str) escrever_byte_tx(*str++);   // Envia vários caracteres em sequência
}

void escrever_calibracao(void) {
    escrever_byte_tx(BYTE_CALIBRACAO_OK);
    escrever_byte_tx(DCOCTL);
    escrever_byte_tx(BCSCTL1);
}
