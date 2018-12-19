using System;
using System.IO;
using System.Net.Sockets;
using TicTacToe.Resolver.Core;

namespace TicTacToe.Server.Handlers
{

    public class TcpConnectionHandler : IConnectionHandler<TcpClient>
    {
        private readonly IRequestResolver _requestResolver;

        public TcpConnectionHandler(IRequestResolver requestResolver)
        {
            _requestResolver = requestResolver;
        }
        public void Handle(TcpClient connection)
        {
            var clientStream = GetClientStream(connection);
            HandleClientStream(clientStream);
            CloseConnection(connection);
        }

        private void HandleClientStream(NetworkStream clientStream)
        {
            using (var streamReader = new StreamReader(clientStream))
            using (var streamWriter = new StreamWriter(clientStream))
            {
                var clientRequest = streamReader.ReadLine();
                Console.WriteLine($"Request: {clientRequest}");
                var response = GetResponse(clientRequest);
                streamWriter.WriteLine(response);
            }
        }    

        private string GetResponse(string clientRequest)
        {
            return _requestResolver.Resolve(clientRequest);
        }

        private NetworkStream GetClientStream(TcpClient connection)
        {
            return connection.GetStream();
        }

        private void CloseConnection(TcpClient connection)
        {
            connection.Close();
        }
    }
}