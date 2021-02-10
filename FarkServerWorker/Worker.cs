using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FarkServerWorker
{
    public class Worker : BackgroundService
    {
        const string IP = "127.0.0.1";
        const int PORT = 5000;

        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Configurações do servidor:");
                _logger.LogInformation("IP: {0}", IP);
                _logger.LogInformation("PORTA: {0}", PORT);

                //var ServerTCP = new TcpListener(IPAddress.Parse(IP), PORT);
                var ServerTCP = new TcpListener(IPAddress.Any, PORT);

                _logger.LogInformation("\n\nIniciando servidor...");
                ServerTCP.Start();
                _logger.LogInformation("Servidor iniciado!");

                _logger.LogInformation("\n\nMensageria: ");

                var BytesReceived = new Byte[256];

                while (true)
                {
                    using var ClienteRecebido = await ServerTCP.AcceptTcpClientAsync();
                    using var Stream = ClienteRecebido.GetStream();

                    int i;
                    while ((i = Stream.Read(BytesReceived, 0, BytesReceived.Length)) != 0)
                    {
                        var DataReceived = Encoding.ASCII.GetString(BytesReceived, 0, i);
                        var DataResponse = Encoding.ASCII.GetBytes(DataReceived.ToUpper());
                        Stream.Write(DataResponse, 0, DataResponse.Length);

                        _logger.LogInformation("Recebido: {0}", DataReceived);
                    }
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
