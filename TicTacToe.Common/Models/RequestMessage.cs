namespace TicTacToe.Common.Models
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
        public string Method { get; set; }
    }
}