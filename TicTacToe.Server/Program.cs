using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Ninject;
using TicTacToe.Server.Managers;
using TicTacToe.Server.Providers;

namespace TicTacToe.Server {
    class Program {
        private const int _listeningPort = 50000;
        private static readonly ITcpServerManager _tcpServerManager;
        private static readonly ITcpListenerProvider _tcpListenerProvider;

        static Program () {
            var kernel = new StandardKernel (new Bindings ());
            _tcpServerManager = kernel.Get<ITcpServerManager>();
            _tcpListenerProvider = kernel.Get<ITcpListenerProvider>();
        }
        static void Main (string[] args)
        {
            StartListening();
        }

        private static void StartListening()
        {
            var server = CreateTcpServer();
            RunServer(server);
        }

        private static TcpListener CreateTcpServer()
        {
            var listeningIP = IPAddress.Parse("127.0.0.1");
            var server = _tcpListenerProvider.CreateListener(listeningIP, _listeningPort);
            return server;
        }

        private static void RunServer(TcpListener server)
        {
            _tcpServerManager.Start(server);
            _tcpServerManager.Listen(server);
        }
    }
}