using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace Pratica_12 {
    class Program {

        static SerialPort serialPort;
        static bool executando = false;
        static bool baudrate_ok = false;
        static bool calibrado_ok = false;
        static string str_buffer = "";
        static int i = 0;

        const int TIMEOUT = 1000; // Timeout em milissegundos
        const byte cal_DCOCTL = 68;
        const byte cal_BCSCTL1 = 135;

        static void Main(string[] args) {
            Console.WriteLine("=============================================");
            Console.WriteLine("               Auto calibrador               ");
            Console.WriteLine("=============================================");

            serialPort = new SerialPort();
            serialPort.BaudRate = 12500;
            serialPort.PortName = "COM4";
            serialPort.DataBits = 8;
            serialPort.Parity = Parity.None;

            serialPort.ReadTimeout = TIMEOUT;
            serialPort.WriteTimeout = TIMEOUT;

            // Loop para encontrar e abrir a porta COM
            while (!executando) {

                /*string[] portas = SerialPort.GetPortNames();
                if (portas.Length == 0) {
                    Console.WriteLine("Nenhuma porta COM encontrada.");
                    Console.WriteLine("Pressione qualquer tecla para tentar encontrar...");
                    Console.ReadLine();
                    return;
                } else if (portas.Length == 1) {
                    string porta = portas[0];
                    Console.WriteLine($"Tentando conectar à '{porta}'.");
                    serialPort.PortName = porta;*/
                    try {
                        serialPort.Open();
                        Console.WriteLine($"Porta Serial '{serialPort.PortName}' aberta!");
                        executando = true;
                    } catch (Exception ex) {
                        Console.WriteLine($"Error: {ex.Message}");
                        Console.WriteLine("Pressione qualquer tecla para tentar novamente...");
                        Console.ReadLine();
                    }/*
                } else {
                    // Multiplas portas encontradas (pode ser implementado no futuro)
                    Console.WriteLine("Mais de uma porta encontrada. Usando a primeira por padrão.");
                    //serialPort.PortName = portas[0];
                    running = true;
                }*/
            }

            // 1. Inicia a leitura de dados em segundo plano
            Task.Run(() => LerDados());

            // 2. Inicia o envio do calibrador em segundo plano
            Task.Run(() => {
                while (executando) {
                    try {
                        // Pula o processo de calibração se o baudrate já estiver confirmado
                        if (!baudrate_ok) {
                            // Envia break
                            serialPort.BreakState = false;
                            serialPort.BreakState = true;
                            EsperarMilissegundos(0.96);

                            // Envia delimitador
                            serialPort.BreakState = false;
                            EsperarMilissegundos(0.16);
                        }

                        // Evnvia o synch byte U (0x55)
                        EnviarByte(0x55);

                        if (!calibrado_ok) {
                            EnviarByte(cal_DCOCTL);
                            EnviarByte(cal_BCSCTL1);
                        }

                        // Envia o valor de calibração (0xAA)
                        EnviarByte(0xAA);

                        EsperarMilissegundos(2.0);

                        // Intervalo entre tentativas de calibração
                        Thread.Sleep(500);
                    } catch (Exception ex) {
                        if (executando) Console.WriteLine($"Erro no ciclo: {ex.Message}");
                    }
                }
            });

            // O Main agora fica livre esperando a tecla de saída
            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();

            executando = false;
            baudrate_ok = true;
            serialPort.Close();
            Console.WriteLine("\nPorta serial fechada. Programa encerrado.");
        }

        static void LerDados() {
            while (executando) {
                try {
                    while (serialPort.BytesToRead > 0) {
                        byte v = (byte)serialPort.ReadByte();

                        // Chegada de calibração
                        if (v == 0xAA) {
                            calibrado_ok = true;
                            baudrate_ok = false; // Irá solicitar uma nova calibração de UART
                            i = 1;  // Prepara para exibir os valores de calibração
                            str_buffer = "";
                            Console.WriteLine("Calibração confirmada!");
                        } else if (i >= 1) {
                            switch (i) {
                                case 1:
                                    str_buffer += $"DCOCTL: {v} || ";
                                    i++;
                                    break;
                                case 2:
                                    str_buffer += $"BCSCTL1: {v}";
                                    Console.WriteLine(str_buffer);
                                    i = 0;
                                    str_buffer = "";
                                    break;
                            }
                        } else if (v == 0x55 /* 'U' */) {
                            baudrate_ok = true;
                            Console.WriteLine("Baudrate confirmado!");
                        } else {
                            str_buffer += (char)v;
                            if (v == '\n') {
                                Console.WriteLine(str_buffer);
                                str_buffer = "";
                            }
                        }
                    }
                } catch (TimeoutException) {
                    // Timeout gerenciado pelo hardware, continua tentando
                } catch (Exception ex) {
                    if (executando) // Evita exibir erro se a porta fechar ao sair
                        Console.WriteLine($"Error na leitura: {ex.Message}");
                }
            }
        }

        static void EnviarByte(byte data) {
            try {
                if (serialPort.IsOpen) serialPort.Write(new[] { data }, 0, 1);
            } catch (Exception ex) {
                if (executando)
                    Console.WriteLine($"Erro ao enviar byte: {ex.Message}");
            }
        }

        // Método de alta precisão baseado no contador de hardware (Busy Waiting)
        static void EsperarMilissegundos(double ms) {
            // Converte milissegundos para a quantidade exata de "Ticks" do processador
            long ticksNecessarios = (long)(ms * Stopwatch.Frequency / 1000);
            Stopwatch sw = Stopwatch.StartNew();

            while (sw.ElapsedTicks < ticksNecessarios) {
                // Alivia levemente o consumo de um núcleo do processador 
                // sem perder a precisão do tempo
                Thread.SpinWait(10);
            }
        }
    }
}