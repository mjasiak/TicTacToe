using System.Net.Sockets;
using TicTacToe.Server.Handlers;

namespace TicTacToe.Server.Listeners
{
    public class TcpServerListener : IServerListener<TcpListener>
    {
        private readonly IConnectionHandler<TcpListener> _tcpConnectionHandler;

        public TcpServerListener(IConnectionHandler<TcpListener> tcpConnectionHandler)
        {
            _tcpConnectionHandler = tcpConnectionHandler;
        }

        public void Listen(TcpListener server)
        {
            while(true){
                _tcpConnectionHandler.Handle(server);
            }
        }
    }
}