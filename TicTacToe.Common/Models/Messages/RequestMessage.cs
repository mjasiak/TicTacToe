namespace TicTacToe.Common.Models.Messages
{
    public sealed class RequestMessage : Message
    {
        public static RequestMessage Empty
        {
            get
            {
                return new RequestMessage
                {
                    Data = string.Empty,
                    Method = string.Empty,
                };
            }
        }
    }
}