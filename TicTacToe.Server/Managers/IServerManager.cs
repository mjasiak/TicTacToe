using System.Net;
using System.Net.Sockets;

namespace TicTacToe.Server.Managers
{
    public interface IServerManager<T>
    {
        void RunServer(T server);
        void StopServer(T server);
    }
}