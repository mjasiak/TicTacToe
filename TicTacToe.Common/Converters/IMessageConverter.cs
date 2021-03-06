using TicTacToe.Common.Models.Messages;

namespace TicTacToe.Common.Converters
{
    public interface IMessageConverter
    {
        string ConvertToJson(object messageToConvert);
        RequestMessage ConvertToRequestMessage(string requestToConvert);
        ResponseMessage ConvertToResponseMessage(string requestToConvert);
    }
}