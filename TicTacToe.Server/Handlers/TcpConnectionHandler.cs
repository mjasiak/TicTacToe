using System;
using System.Net.Sockets;
using TicTacToe.Resolver.Core;

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
            var response = GetResponse();
            SendResponse(response);
            CloseConnection();
        }

        private void SendResponse(string response)
        {
            throw new NotImplementedException();
        }

        private void GetClientConnection(TcpListener server)
        {
            _connection = server.AcceptTcpClient();
        }

        private string GetResponse()
        {
            var requestStream = GetRequestStream();
            return _requestResolver.Resolve(requestStream);
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

        public bool IsConnectionActive()
        {
            if (_connection != null)
            {
                return _connection.Connected;
            }
            else
            {
                return false;
            }
        }
    }
}