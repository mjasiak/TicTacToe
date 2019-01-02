using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using TicTacToe.Server.Handlers;
using TicTacToe.Server.Providers;

namespace TicTacToe.Server.Managers
{
    public class TcpServerManager : IServerManager<TcpListener>
    {
        private readonly ITcpListenerProvider _tcpListenerProvider;
        private readonly IConnectionHandler<TcpClient> _tcpConnectionHandler;

        public TcpServerManager(ITcpListenerProvider tcpListenerProvider, IConnectionHandler<TcpClient> tcpConnectionHandler)
        {
            _tcpListenerProvider = tcpListenerProvider;
            _tcpConnectionHandler = tcpConnectionHandler;
        }
        public void RunServer(TcpListener server)
        {
            StartServer(server);
            StartListening(server);
        }

        private void StartListening(TcpListener server)
        {
            Console.WriteLine("Server is listening for incoming connections.");
            while(true){
                var connection = GetClientConnection(server);
                _tcpConnectionHandler.Handle(connection);
                Console.WriteLine("Client connection handled...");
                Console.WriteLine("Waiting for new connection...");
            }
        }

        private TcpClient GetClientConnection(TcpListener server){           
            return server.AcceptTcpClient();
        }

        private void StartServer(TcpListener server)
        {
                server.Start();
                Console.WriteLine("Server has started.");
        }

        public void StopServer(TcpListener server)
        {
            server.Stop();
        }
    }
}