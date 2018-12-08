using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Ninject;
using TicTacToe.Server.Managers;
using TicTacToe.Server.Providers;

namespace TicTacToe.Server
{
    class Program
    {
        private const string _listeningAddress = "127.0.0.1";
        private const int _listeningPort = 50000;
        private static readonly IServerManager<TcpListener> _serverManager;
        private static readonly ITcpListenerProvider _tcpListenerProvider;

        static Program()
        {
            var kernel = new StandardKernel(new Bindings());
            _serverManager = kernel.Get<IServerManager<TcpListener>>();
            _tcpListenerProvider = kernel.Get<ITcpListenerProvider>();
        }
        static void Main(string[] args)
        {
            var server = CreateListener(_listeningAddress, _listeningPort);
            RunTcpServer(server);
            Console.WriteLine("Server stopped running. Press any key to close.");
            Console.ReadKey();
        }

         private static TcpListener CreateListener(string listeningIPAddress, int listeningPort)
        {
            return _tcpListenerProvider.CreateListener(listeningIPAddress, listeningPort);
        }

        private static void RunTcpServer(TcpListener server)
        {
            _serverManager.RunServer(server);
        }
    }
}