using Newtonsoft.Json;
using TicTacToe.Common.Models;

namespace TicTacToe.Common.Converters
{
    public class MessageConverter : IMessageConverter
    {
        public string ConvertToJson(object messageToConvert)
        {
            return JsonConvert.SerializeObject(messageToConvert);
        }

        public RequestMessage ConvertToRequestMessage(string requestToConvert)
        {
            try
            {
                return JsonConvert.DeserializeObject<RequestMessage>(requestToConvert);
            }
            catch
            {
                return RequestMessage.Empty;
            }
        }

        public ResponseMessage ConvertToResponseMessage(string requestToConvert)
        {
            return JsonConvert.DeserializeObject<ResponseMessage>(requestToConvert);
        }
    }
}