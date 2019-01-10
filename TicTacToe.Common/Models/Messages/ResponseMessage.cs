using Newtonsoft.Json;
using TicTacToe.Common.Enums;

namespace TicTacToe.Common.Models.Messages
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
                    Method = string.Empty,
                    InnerMethod = string.Empty,
                    Status = MessageStatus.Failure,
                    Text = string.Empty,
                    Broadcast = false
                };
            }
        }
        public MessageStatus Status { get; set; }
        public string Text { get; set; } = string.Empty;
        public string InnerMethod { get; set; } = string.Empty;
        public bool Broadcast { get; set; } = true;
    }
}