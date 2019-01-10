using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TicTacToe.Common
{
    public class ClientConnection
    {
        public ClientConnection(TcpClient clientSocket)
        {
            Id = Guid.NewGuid();
            Socket = clientSocket;
        }

        public Guid Id { get; private set; }
        public TcpClient Socket { get; private set; }


        public static ClientConnection Connect(string ipAddress, int port)
        {
            var socket = new TcpClient();
            socket.Connect(ipAddress, port);
            return new ClientConnection(socket);
        }
    }
}