using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
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
            Console.WriteLine("Client connected...");
            var clientRequest = GetClientRequest(clientStream);
            Console.WriteLine($"Request: {clientRequest}");
            var response = GetResponse(clientRequest);
            SendResponse(clientStream, response);
        }

        private void SendResponse(NetworkStream clientStream, string response)
        {
            var streamWriter = new StreamWriter(clientStream);

            streamWriter.WriteLine(response);
            streamWriter.Flush();
        }

        private string GetClientRequest(NetworkStream clientStream)
        {
            using (var catchedStream = new MemoryStream())
            {
                byte[] data = new byte[1024];
                do
                {
                    clientStream.Read(data, 0, data.Length);
                    catchedStream.Write(data, 0, data.Length);
                } while (clientStream.DataAvailable);

                Console.WriteLine("Data read");
                return Encoding.ASCII.GetString(catchedStream.GetBuffer(), 0, (int)catchedStream.Length);
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
            Console.WriteLine("Connection closed.");
        }
    }
}