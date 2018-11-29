using System;
using System.IO;
using TicTacToe.Common.Models;

namespace TicTacToe.Server.Resolvers
{
    public class RequestResolver : IRequestResolver
    {
        public void Resolve(Stream clientStream)
        {
            var convertedMessage = GetConvertedMessage(clientStream);
            SendResponseToClient(clientStream,convertedMessage);
        }

        private void SendResponseToClient(Stream clientStream, string convertedMessage)
        {
            using(var streamWriter = new StreamWriter(clientStream)){
                streamWriter.WriteLine(convertedMessage);
            }
        }

        private string GetConvertedMessage(Stream clientStream)
        {
            using (var streamReader = new StreamReader(clientStream))
            {
                var clientRequest = streamReader.ReadLine();
                //var responseMessage = _requestProcessor.GetResponse(clientStream);
                var convertedMessage = string.Empty; //= _messageConverter.GetConvertedMessage(responseMessage);    
                return convertedMessage;
            }
        }
    }
}