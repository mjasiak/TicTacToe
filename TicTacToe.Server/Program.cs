using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using TicTacToe.Common;
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
        }
        static void Main(string[] args)
        {
            RunServer(_listeningAddress, _listeningPort);
        }

        private static void RunServer(string listeningAddress, int listeningPort)
        {
            _serverManager.RunServer(listeningAddress, listeningPort);
        }   
    }   
}