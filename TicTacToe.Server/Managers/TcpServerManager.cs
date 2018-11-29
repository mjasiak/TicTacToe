using System.IO;
using System.Net;
using System.Net.Sockets;
using TicTacToe.Server.Handlers;
using TicTacToe.Server.Providers;
using TicTacToe.Server.Resolvers;

namespace TicTacToe.Server.Managers {
    public class TcpServerManager : ITcpServerManager {
        private readonly ITcpConnectionHandler _tcpConnectionHandler;

        public TcpServerManager(ITcpConnectionHandler tcpConnectionHandler)
        {
            _tcpConnectionHandler = tcpConnectionHandler;
        }
        public void Start(TcpListener server)
        {
            server.Start();
        }
        public void Stop(TcpListener server) {
            server.Stop();
        }

        public void Listen(TcpListener server) {
            while (true)
            {
                _tcpConnectionHandler.Handle(server);
            }
        }
    }
}