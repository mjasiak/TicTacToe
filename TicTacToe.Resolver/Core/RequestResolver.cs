using System;
using System.IO;
using TicTacToe.Common.Models;
using TicTacToe.Resolver.Converters;
using TicTacToe.Resolver.Managers;

namespace TicTacToe.Resolver.Core
{
    public class RequestResolver : IRequestResolver
    {
        private readonly IMessageConverter _messageConverter;
        private readonly IPlayersManager _playersManager;

        public RequestResolver(IMessageConverter messageConverter, IPlayersManager playersManager)
        {
            _messageConverter = messageConverter;
            _playersManager = playersManager;
        }
        public string Resolve(string clientRequest)
        {
            RequestMessage requestMessage = GetRequestMessage(clientRequest);
            ResponseMessage responseMessage = ResponseMessage.Empty;

            switch (requestMessage.Method)
            {
                case "users/show":
                    {
                        responseMessage = _playersManager.ShowConnectedPlayers();
                        break;
                    }
                case "user/add":
                    {
                        responseMessage = _playersManager.AddPlayer(requestMessage.Data);
                        break;
                    }
            }

            return ConvertResponseMessage(responseMessage);
        }

        private string ConvertResponseMessage(ResponseMessage responseMessage)
        {
            return _messageConverter.ConvertToJson(responseMessage);
        }

        private RequestMessage GetRequestMessage(string clientRequest)
        {
            return _messageConverter.ConvertToRequestMessage(clientRequest);
        }
    }
}