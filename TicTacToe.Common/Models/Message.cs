using System;
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
                    Data = string.Empty,
                    DataType = typeof(string),
                    Format = MessageFormat.JSON,
                    Status = MessageStatus.Failure,
                };
            }
        }
        string Data { get; set; }
        Type DataType { get; set; }
        MessageFormat Format { get; set; }
        MessageStatus Status { get; set; }
    }
}