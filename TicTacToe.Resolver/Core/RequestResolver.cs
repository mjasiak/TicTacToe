using TicTacToe.Common.Converters;
using TicTacToe.Common.Models.Messages;
using TicTacToe.Resolver.Managers;

namespace TicTacToe.Resolver.Core
{
    public class RequestResolver : IRequestResolver
    {
        private readonly IMessageConverter _messageConverter;
        private readonly IPlayersManager _playersManager;
        private readonly IGameManager _gameManager;

        public RequestResolver(IMessageConverter messageConverter, IPlayersManager playersManager, IGameManager gameManager)
        {
            _messageConverter = messageConverter;
            _playersManager = playersManager;
            _gameManager = gameManager;
        }
        public ResponseMessage Resolve(string clientRequest)
        {
            RequestMessage requestMessage = GetRequestMessage(clientRequest);
            ResponseMessage responseMessage = ResponseMessage.Empty;

            switch (requestMessage.Method)
            {
                case "game/start":
                    {
                        responseMessage = _gameManager.Start();
                        break;
                    }
                case "player/add":
                    {
                        responseMessage = _playersManager.AddPlayer(requestMessage.Data);
                        break;
                    }
                case "game/move":
                    {
                        responseMessage = _gameManager.Move(requestMessage.Data);
                        break;
                    }
            }

            return responseMessage;
        }

        private RequestMessage GetRequestMessage(string clientRequest)
        {
            return _messageConverter.ConvertToRequestMessage(clientRequest);
        }
    }
}