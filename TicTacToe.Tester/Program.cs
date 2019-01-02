using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TicTacToeTester
{
    class Program
    {
        private static string _listeningAddress = "127.0.0.1";
        private static int _listeningPort = 50000;
        static void Main(string[] args)
        {
            Console.WriteLine("Starting client...");
            Console.WriteLine($"Connecting with server on {_listeningAddress}:{_listeningPort}");

            string message = string.Empty;
            message = Console.ReadLine();

            while (!message.Equals("exit"))
            {
                var client = new TcpClient(_listeningAddress, _listeningPort);
                Console.WriteLine($"Connected with server on {_listeningAddress}:{_listeningPort}");
                var stream = client.GetStream();
                var streamWriter = new StreamWriter(stream);
                streamWriter.Write(message);
                streamWriter.Flush();
                var response = GetClientRequest(stream);
                Console.WriteLine($"Response: {response}");
                Console.WriteLine("New command: ");
                message = Console.ReadLine();
            }
            Console.WriteLine("Client has exited.");
            Console.ReadKey();
        }
        private static string GetClientRequest(NetworkStream clientStream)
        {
            using (var catchedStream = new MemoryStream())
            {  
                byte[] data = new byte[1024];
                do
                {
                    clientStream.Read(data, 0, data.Length);
                    catchedStream.Write(data, 0, data.Length);
                } while (clientStream.DataAvailable);
                return Encoding.ASCII.GetString(catchedStream.GetBuffer(), 0, (int)catchedStream.Length);
            }
        }
    }
}
