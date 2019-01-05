using System;
using Newtonsoft.Json;

namespace TicTacToe.Common.Models
{
    public abstract class Message
    {
        public string Data { get; set; }
        public string Method { get; set; }
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}