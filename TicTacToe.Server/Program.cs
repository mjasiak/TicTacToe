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
        private static readonly IServerManager _serverManager;

        static Program()
        {
            var kernel = new StandardKernel(new Bindings());
            _serverManager = kernel.Get<IServerManager>();
        }
        static void Main(string[] args)
        {
            RunTcpServer(_listeningAddress, _listeningPort);
        }

        private static void RunTcpServer(string listeningIPAddress, int listeningPort)
        {
            _serverManager.RunServer(listeningIPAddress, listeningPort);
        }
    }
}