using System;
using System.Net;

namespace TicTacToe.Server
{
    class Program
    {
        private const int _listeningPort = 50000;
        static void Main(string[] args)
        {
            var listeningIP = IPAddress.Parse("127.0.0.1");
            var listener = new TcpListener(listeningIP, _listeningPort);
            listener.Start();
            while(true){
                var client = listener.Accept
            }
            Console.WriteLine("Hello World!");
        }
    }
}
