using System.Net.Sockets;
using TicTacToe.Server.Resolvers;

namespace TicTacToe.Server.Handlers
{

    public class TcpConnectionHandler : ITcpConnectionHandler
    {
        private readonly IRequestResolver _requestResolver;

        public TcpConnectionHandler(IRequestResolver requestResolver)
        {
            _requestResolver = requestResolver;
        }
        public void Handle(TcpListener server)
        {
            var connection = GetClientConnection(server);
            ResolveRequest(connection);
            CloseConnection(connection);
        }

        private TcpClient GetClientConnection(TcpListener server)
        {
            return server.AcceptTcpClient();
        }
        
        private void ResolveRequest(TcpClient connection)
        {
            var requestStream = GetRequestStream(connection);
            _requestResolver.Resolve(requestStream);
        }

        private NetworkStream GetRequestStream(TcpClient client)
        {
            var clientStream = client.GetStream();
            return clientStream;
        }

        private void CloseConnection(TcpClient connection)
        {
            connection.Close();
        }
    }
}