using Newtonsoft.Json;
using TicTacToe.Common.Models;

namespace TicTacToe.Resolver.Converters
{
    public class MessageConverter : IMessageConverter
    {
        public string ConvertToJson(object messageToConvert)
        {
            return JsonConvert.SerializeObject(messageToConvert);
        }

        public RequestMessage ConvertToRequestMessage(string requestToConvert)
        {
            return JsonConvert.DeserializeObject<RequestMessage>(requestToConvert);
        }

        public ResponseMessage ConvertToResponseMessage(string requestToConvert)
        {
            return JsonConvert.DeserializeObject<ResponseMessage>(requestToConvert);
        }
    }
}