using System.Net.Sockets;

namespace TicTacToe.Server.Handlers
{
    public interface ITcpConnectionHandler
    {
        void Handle(TcpListener server);
    }
}