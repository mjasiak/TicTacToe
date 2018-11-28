using TicTacToe.Common.Enums;

namespace TicTacToe.Common.Models {
    public class Message {
        MessageStatus Status { get; set; }
        string Data {get;set;}
    }
}