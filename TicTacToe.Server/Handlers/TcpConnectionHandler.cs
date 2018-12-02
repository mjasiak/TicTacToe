using System.Net.Sockets;
using TicTacToe.Server.Resolvers;

namespace TicTacToe.Server.Handlers
{

    public class TcpConnectionHandler : IConnectionHandler<TcpListener>
    {
        private readonly IRequestResolver _requestResolver;
        private TcpClient _connection;

        public TcpConnectionHandler(IRequestResolver requestResolver)
        {
            _requestResolver = requestResolver;
        }
        public void Handle(TcpListener server)
        {
            GetClientConnection(server);
            ResolveRequest();
            CloseConnection();
        }

        private void GetClientConnection(TcpListener server)
        {
            _connection = server.AcceptTcpClient();
        }
        
        private void ResolveRequest()
        {
            var requestStream = GetRequestStream();
            _requestResolver.Resolve(requestStream);
        }

        private NetworkStream GetRequestStream()
        {
            var clientStream = _connection.GetStream();
            return clientStream;
        }

        private void CloseConnection()
        {
            _connection.Close();
        }

        public bool IsConnectionActive(){
            return _connection.Connected;
        }
    }
}