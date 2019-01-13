using System;
using System.Collections.Generic;

namespace TicTacToe.Common.Models
{
    public class Game
    {
        public Game(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
        public List<Player> Players { get; set; }
        public char[] Map { get; set; }
        public char Turn { get; set; } = 'X';
        public bool Finished
        {
            get
            {
                if (Winner.Equals('N'))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public char Winner { get; set; } = 'N';
    }
}