using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using TicTacToe.Common;
using TicTacToe.Common.Models;
using TicTacToe.Server.Handlers;
using TicTacToe.Server.Providers;

namespace TicTacToe.Server.Managers
{
    public class TcpServerManager : IServerManager<TcpListener>
    {
        private TcpListener _server;
        private readonly ITcpListenerProvider _tcpListenerProvider;
        private readonly IConnectionHandler<TcpClient> _tcpConnectionHandler;
        // private List<UserConnection> _connectedUsers = new List<UserConnection>();

        public TcpServerManager(ITcpListenerProvider tcpListenerProvider, IConnectionHandler<TcpClient> tcpConnectionHandler)
        {
            _tcpListenerProvider = tcpListenerProvider;
            _tcpConnectionHandler = tcpConnectionHandler;
        }
        public void RunServer(string ipaddress, int port)
        {
            _server = CreateServer(ipaddress, port);
            StartServer();
            StartListening();
        }

        private void StartListening()
        {
            while (true)
            {
                var client = _server.AcceptTcpClient();
                _tcpConnectionHandler.HandleConnection(client);
                // var connection = new ClientConnection(client);
                // var connectionHandler = new ClientConnectionHandler(connection);
                // var name = connectionHandler.Receive();

                // _connectedUsers.Add(new UserConnection(name, connection));

                // connectionHandler.OnMessageReceived += connectionHandler_OnMessageReceived;
                // connectionHandler.StartListening();
            }
        }

        private void StartServer()
        {
                _server.Start();
                Console.WriteLine("* Server has started *");
        }

        private TcpListener CreateServer(string listeningIPAddress, int listeningPort)
        {
            return _tcpListenerProvider.CreateListener(listeningIPAddress, listeningPort);
        }

        public void StopServer()
        {
            _server.Stop();
            Console.WriteLine("* Server stopped running. Press any key to close... *");
            Console.Read();
        }

        // private void connectionHandler_OnMessageReceived(object source, string message)
        // {
        //     var clientConnectionHandler = source as ClientConnectionHandler;

        //     Console.WriteLine(string.Format("Server got message: {0}", message));
        //     if (_connectedUsers.Count <= 1)
        //     {
        //         clientConnectionHandler.Send("You're only one connected client.");
        //     }
        //     else
        //     {
        //         foreach (var user in _connectedUsers)
        //         {
        //             if (user.Connection.Id != clientConnectionHandler.Connection.Id)
        //             {
        //                 var userConnectionHandler = new ClientConnectionHandler(user.Connection);
        //                 userConnectionHandler.Send(string.Format("{0}: {1}", user.Name, message));
        //             }
        //         }
        //     }
        // }
    }
}