using System.Net;
using System.Net.Sockets;

namespace TicTacToe.Server.Managers
{
    public interface ITcpServerManager
    {
        void Start(TcpListener server);
        void Stop(TcpListener server);
        void Listen(TcpListener server);
    }
}