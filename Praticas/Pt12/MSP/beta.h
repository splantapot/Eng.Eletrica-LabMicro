/**
 * main.c
 */

//powershell.exe -ExecutionPolicy Bypass -File "C:\flash-ch340.ps1" "${CG_TOOL_HEX}" "${BuildArtifactFileName}" "${BuildArtifactFileBaseName}"

#include <msp430.h>

#define RXD BIT1 // P1.1 = UCA0RXD
#define TXD BIT2 // P1.2 = UCA0TXD

#define tp BIT3 //transistor bc639 de programacao
#define tu BIT4 //transistor bc337 de utilizacaoo

volatile unsigned char lin_rx_buffer[12];
volatile unsigned char lin_rx_buffer_teste[50];
volatile unsigned char lin_rx_index = 0;
volatile unsigned char i = 0;
volatile unsigned char lin_frame_ready = 0;
volatile unsigned char baud_ok = 0;
volatile unsigned char teve_break = 0;
volatile unsigned char valor_x = 0, comp = 0x55;

void main(void) {
    WDTCTL = WDTPW + WDTHOLD;
    UCA0RXBUF = 9;
    lin_rx_buffer[0] =9; lin_rx_buffer[1] =9;
    // Clock 1MHz
    DCOCTL = 68;   BCSCTL1 = 135;
    //o
      P1DIR &= ~BIT5;          //entrada
      P1REN &= ~(BIT5 + BIT2); //sem
      P1REN |= BIT1;  //<<<<<<<<<habilita resistor de pullup no pino RX do msp<<<<<<<<<<<<<
      P1DIR |= tp + tu;//s
      P1REN &= ~tp;       //sem resistor
      P1OUT &= ~tp;       //n
      P1REN |= tu;        //resistor on
      P1OUT |= tu | BIT1;////<<<<<<<<<<<

    // Pinos UART
    P1SEL |= RXD + TXD;
    P1SEL2 |= RXD + TXD;
    P1DIR |= TXD;

    UCA0CTL1 |= UCSWRST;

    UCA0CTL0 = UCMODE_3;                // Modo LIN
    UCA0CTL1 |= UCSSEL_2 | UCBRKIE;     //SMCLK e <<<<<<<<<habilitada a detecÃ§Ã£o de break!!!!!!!!!!!!!!

    // Valores iniciais ( sobrescritos pelo auto-baud)
    UCA0BR0 = 80;
    UCA0BR1 = 0x00;
    // UCA0MCTL = UCBRS_1;

    UCA0ABCTL = UCABDEN; // Auto Baud Detection

    // Interrupcoes
    // UCA0CTL1 |= UCBRKIE; // Break

    UCA0CTL1 &= ~UCSWRST;

    IE2 |= UCA0RXIE; // RX normal
    __bis_SR_register(GIE);

    // Configurar LEDs
    P2DIR |= BIT4;
    P2REN |= BIT4;
    P2OUT &= ~BIT4;

    P3DIR |= BIT6 + BIT7;
    P3REN |= BIT6 + BIT7;
    P3OUT &= ~(BIT6 + BIT7);

    while(1) {
        //if (lin_frame_ready) {
            //P2OUT = (lin_rx_buffer[1] == 0xAA)? P2OUT | BIT4 : P2OUT & (~BIT4);
            if (lin_rx_buffer[1] == 0xAA) {
                P2OUT |= BIT4;
            } else {
                P2OUT &= ~BIT4;
            }
            while (!(IFG2 & UCA0TXIFG));  // Aguarda buffer livre
            UCA0TXBUF = 'E';
            // === Processar frame LIN aqui ===
            // lin_rx_buffer[0] = PID, etc.
            lin_frame_ready = 0;
            lin_rx_index = 0;
        //}
    }
}

// ===================================================================
#pragma vector=USCIAB0RX_VECTOR
__interrupt void USCI0RX_ISR(void) {
    teve_break = UCA0STAT & UCBRK;  // Break é apontado quando o 0x55 (U) é lido.
    unsigned char rx_data = UCA0RXBUF; // Le e limpa flag
    P3OUT ^= BIT6;
  //  if (i < 50) lin_rx_buffer_teste[i++] = rx_data;




    // ------------------- BREAK -------------------
    //   P3OUT |= BIT6;
    if (teve_break) {
        UCA0STAT &= ~UCBRK; // Limpa flag de Break

        lin_rx_index = 1;
        baud_ok = (rx_data == 0x55);

        // Garante que a interrupcao de RX está habilitada para receber o Sync
        IE2 |= UCA0RXIE; //ie1 ou ie2

        P3OUT |= BIT7;
        //lin_rx_buffer[lin_rx_index++] = rx_data;
        return;
    }
    if (1){
        lin_rx_buffer[lin_rx_index++] = rx_data;
    }
    // ------------------- BYTE NORMAL -------------------
    //lin_rx_buffer[lin_rx_index++] = rx_data;
    //if (lin_rx_index >= 5) lin_rx_index = 0;
    /*
    if (lin_rx_index == 0) { // Deve ser o campo Sync (0x55)
        P3OUT &= ~BIT7;
        if (rx_data == 0x55) {
            baud_ok = 1;
            P3OUT |= BIT6;
            // Sync OK → continua recebendo os proximos bytes
        } else {
            // Sync invalidos → descarta frame e desabilita RX ate proximo Break
            baud_ok = 0;
           // UC0IE &= ~UCA0RXIE; // Desabilita interrupcao de recepcao
            return;
        }
    } else if (baud_ok) {
        // Armazena dados do frame (PID + dados + checksum)
        if (lin_rx_index < sizeof(lin_rx_buffer)) {
            lin_rx_buffer[lin_rx_index] = rx_data;
        }
    }

    lin_rx_index++;
    if (lin_rx_index >= 5) lin_rx_index = 0;

    // Exemplo: frame com PID + 8 bytes de dados + checksum (ajuste conforme necessário)
    /*if (lin_rx_index >= 10) {
        lin_frame_ready = 1;
        // Opcional: desabilitar RXIE até próximo Break se quiser processar um frame por vez
        // UC0IE &= \~UCA0RXIE;
    }*/
}
