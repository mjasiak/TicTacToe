using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
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

        private async void StartListening(TcpListener server)
        {
            Console.WriteLine("Server is listening for incoming connections.");
            while(true){
                var connection = await GetClientConnectionAsync(server);
                await _tcpConnectionHandler.HandleConnection(connection);
                Console.WriteLine("Client connection handled...");
            }
        }

        private async Task<TcpClient> GetClientConnectionAsync(TcpListener server){           
            return await server.AcceptTcpClientAsync();
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