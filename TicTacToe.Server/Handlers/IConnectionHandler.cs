using System.Net.Sockets;

namespace TicTacToe.Server.Handlers
{
    public interface IConnectionHandler<T>
    {
        void Handle(T server);
    }
}