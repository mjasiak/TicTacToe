using System;
using System.IO;
using TicTacToe.Common.Models;
using TicTacToe.Resolver.Converters;

namespace TicTacToe.Resolver.Core
{
    public class RequestResolver : IRequestResolver
    {
        private readonly IMessageConverter _messageConverter;

        public RequestResolver(IMessageConverter messageConverter)
        {
            _messageConverter = messageConverter;
        }
        public string Resolve(Stream requestStream)
        {
            using (var streamReader = new StreamReader(requestStream))
            {
                RequestMessage requestMessage = GetRequestMessage(streamReader);
                ResponseMessage responseMessage = ResponseMessage.Empty;

                switch (requestMessage.Method)
                {
                    case "users/show":
                        {
                            //responseMessage = _usersManager.ShowConnectedUsers();
                            break;
                        }
                    case "user/add":
                        {
                            //responseMessage = _usersManager.AddUser(requestMessage.Data);
                            break;
                        }
                }

                return ConvertResponseMessage(responseMessage);
            }
        }

        private string ConvertResponseMessage(ResponseMessage responseMessage)
        {
            return _messageConverter.ConvertToJson(responseMessage);
        }

        private RequestMessage GetRequestMessage(StreamReader streamReader)
        {
            var clientRequest = streamReader.ReadLine();
            return _messageConverter.ConvertToRequestMessage(clientRequest);
        }
    }
}