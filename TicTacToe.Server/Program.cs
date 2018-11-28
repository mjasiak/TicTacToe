using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

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
                var client = listener.AcceptTcpClient();
                var stream = client.GetStream();
                using(var streamReader = new StreamReader(stream)){

                }
            }
        }
    }
}
