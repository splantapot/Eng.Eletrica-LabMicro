using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace Pratica_12 {
    class Program {

        static SerialPort serialPort;
        static bool running = false;

        const int TIMEOUT = 1000; // Timeout em milissegundos

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
            while (!running) {

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
                        running = true;
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

            // 2. Inicia o envio do calibrador em segundo plano para não travar o Main
            Task.Run(() => {
                while (running) {
                    EnviarBreak();
                    EnviarDelimitador();
                    EnviarByte(0x55);
                    EnviarByte(0xAA);

                    Thread.Sleep(10);
                }
            });

            // O Main agora fica livre esperando a tecla de saída
            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();

            running = false;
            serialPort.Close();
            Console.WriteLine("Porta serial fechada. Programa encerrado.");
        }

        static void LerDados() {
            while (running) {
                try {
                    string data = serialPort.ReadExisting();
                    if (!string.IsNullOrEmpty(data)) {
                        Console.WriteLine($"DT: {data}");
                        continue;
                    }
                } catch (TimeoutException) {
                    // Timeout gerenciado pelo hardware, continua tentando
                } catch (Exception ex) {
                    if (running) // Evita exibir erro se a porta fechar ao sair
                        Console.WriteLine($"Error na leitura: {ex.Message}");
                }
            }
        }

        static void EnviarBreak() {
            serialPort.BreakState = true;
            EsperarMilissegundos(1); // Crava 1 ms em nível BAIXO
            serialPort.BreakState = false;
        }

        static void EnviarDelimitador() {
            // Garante que o BreakState está desligado (nível ALTO)
            serialPort.BreakState = false;
            EsperarMilissegundos(1); // Crava 1 ms em nível ALTO
        }

        static void EnviarByte(byte data) {
            try {
                serialPort.Write(new[] { data }, 0, 1);
            } catch (Exception ex) {
                if (running)
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