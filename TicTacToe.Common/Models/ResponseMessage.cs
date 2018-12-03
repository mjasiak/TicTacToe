using TicTacToe.Common.Enums;

namespace TicTacToe.Common.Models
{
    public class ResponseMessage : Message
    {
        public static ResponseMessage Empty
        {
            get
            {
                return new ResponseMessage
                {
                    Data = string.Empty,
                    Status = MessageStatus.Failure,
                    Text = string.Empty
                };
            }
        }
        public MessageStatus Status { get; set; }
        public string Text { get; set; }
    }
}