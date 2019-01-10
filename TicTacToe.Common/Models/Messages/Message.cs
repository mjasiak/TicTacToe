using System;
using Newtonsoft.Json;

namespace TicTacToe.Common.Models.Messages
{
    public abstract class Message
    {
        public string Data { get; set; } = string.Empty;
        public bool Hidden { get; set; } = false;
        public string Method { get; set; }
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}