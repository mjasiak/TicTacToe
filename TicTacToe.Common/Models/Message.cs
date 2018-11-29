using TicTacToe.Common.Enums;

namespace TicTacToe.Common.Models
{
    public class Message
    {
        public static Message Empty
        {
            get
            {
                return new Message
                {
                    Status = MessageStatus.Success,
                    Data = string.Empty
                };
            }
        }
        MessageStatus Status { get; set; }
        string Data { get; set; }
    }
}