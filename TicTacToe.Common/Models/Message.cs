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
                    Status = MessageStatus.Failure,
                    Data = string.Empty,
                    DataType = typeof(string)
                };
            }
        }
        MessageStatus Status { get; set; }
        string Data { get; set; }
        Type DataType {get;set;}
    }
}