using System.Net.Sockets;
using System.Threading.Tasks;

namespace TicTacToe.Server.Handlers
{
    public interface IConnectionHandler<T>
    {
        void HandleConnection(T client);
    }
}