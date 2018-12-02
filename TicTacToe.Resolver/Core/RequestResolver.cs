using System;
using System.IO;
using TicTacToe.Common.Models;
using TicTacToe.Resolver.Processor;

namespace TicTacToe.Resolver.Core
{
    public class RequestResolver : IRequestResolver
    {
        private readonly IRequestProcessor _requestProcessor;

        public RequestResolver(IRequestProcessor requestProcessor)
        {
            _requestProcessor = requestProcessor;
        }
        public void Resolve(Stream clientStream)
        {
            var responseMessage = GetResponseMessage(clientStream);
            SendResponseToClient(clientStream, responseMessage);
        }

        private void SendResponseToClient(Stream clientStream, string responseMessage)
        {
            using (var streamWriter = new StreamWriter(clientStream))
            {
                streamWriter.WriteLine(responseMessage);
            }
        }

        private string GetResponseMessage(Stream clientStream)
        {
            using (var streamReader = new StreamReader(clientStream))
            {
                var clientRequest = streamReader.ReadLine();
                var responseMessage = _requestProcessor.GetResponse(clientRequest);
                return responseMessage;
            }
        }
    }
}