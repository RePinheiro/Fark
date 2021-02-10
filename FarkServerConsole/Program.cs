using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace FarkServerConsole
{
    class Program
    {
        const string IP = "127.0.0.1";
        const int PORT = 5000;

        static void Main(string[] args)
        {
            Console.WriteLine("Configurações do servidor:");
            Console.WriteLine("IP: {0}", IP);
            Console.WriteLine("PORTA: {0}", PORT);

            var ServerTCP = new TcpListener(IPAddress.Parse(IP), PORT);

            Console.WriteLine("\n\nIniciando servidor...");
            ServerTCP.Start();
            Console.WriteLine("Servidor iniciado!");

            Console.WriteLine("\n\nMensageria: ");

            var BytesReceived = new Byte[256];

            while (true)
            {
                using var ClienteRecebido = ServerTCP.AcceptTcpClient();
                using var Stream = ClienteRecebido.GetStream();

                int i;
                while ((i = Stream.Read(BytesReceived, 0, BytesReceived.Length)) != 0)
                {
                    var DataReceived = Encoding.ASCII.GetString(BytesReceived, 0, i);
                    var DataResponse = Encoding.ASCII.GetBytes(DataReceived.ToUpper());
                    Stream.Write(DataResponse, 0, DataResponse.Length);

                    Console.WriteLine("Recebido: {0}", DataReceived);
                }

            }
        }
    }
}

