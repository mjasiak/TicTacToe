using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Newtonsoft.Json;
using TicTacToe.Common;
using TicTacToe.Common.Enums;
using TicTacToe.Common.Models;
using TicTacToe.Resolver.Core;

namespace TicTacToe.Server.Handlers
{

    public class TcpConnectionHandler : IConnectionHandler<TcpClient>
    {
        private readonly IRequestResolver _requestResolver;
        private List<ClientConnection> _activeConnections = new List<ClientConnection>();

        public TcpConnectionHandler(IRequestResolver requestResolver)
        {
            _requestResolver = requestResolver;
        }

        public void HandleConnection(TcpClient client)
        {
            var connection = new ClientConnection(client);
            _activeConnections.Add(connection);

            var connectionHandler = new ClientConnectionHandler(connection);

            connectionHandler.OnMessageReceived += connectionHandler_OnMessageReceived;
            connectionHandler.StartListening();
        }

        private void connectionHandler_OnMessageReceived(object source, string requestMessage)
        {
            var clientConnectionHandler = source as ClientConnectionHandler;
            var responseMessage = _requestResolver.Resolve(requestMessage);

            var serializedResponseMessage = JsonConvert.SerializeObject(responseMessage);
            clientConnectionHandler.Send(serializedResponseMessage);
            if (responseMessage.Status == MessageStatus.Success)
            {
                foreach (var connection in _activeConnections)
                {
                    if (connection.Id != clientConnectionHandler.Connection.Id)
                    {
                        var userConnectionHandler = new ClientConnectionHandler(connection);
                        userConnectionHandler.Send(serializedResponseMessage);
                    }
                }
            }

        }

    }
}