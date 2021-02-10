using System;
using System.Net.Sockets;
using System.Text;

namespace FarkCliente
{
    class Program
    {
        const string IP = "191.235.228.33";
        const int PORT = 80;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Mensagem: ");

                var Response = SendData(Console.ReadLine());

                Console.WriteLine("Resposta: {0}", Response);
                Console.ReadKey();
                Console.Clear();
            }
        }

        static string SendData(string Mensagem)
        {
            
            using var ClienteTCP = new TcpClient(IP, PORT);
            using var Stream = ClienteTCP.GetStream();

            //Request
            var DataRequest = Encoding.ASCII.GetBytes(Mensagem);
            Stream.Write(DataRequest, 0, DataRequest.Length);

            //Response
            var DataResponse = new Byte[256];
            var BytesResponse = Stream.Read(DataResponse, 0, DataResponse.Length);
            return Encoding.ASCII.GetString(DataResponse, 0, BytesResponse);
        }
    }
}
