using System.Collections.Generic;

namespace TicTacToe.Common.Models
{
    public class Game
    {
        public List<Player> Players { get; set; }
        public string[] Map { get; set; }
    }
}