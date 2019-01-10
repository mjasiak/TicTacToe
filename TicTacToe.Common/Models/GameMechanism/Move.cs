using System;

namespace TicTacToe.Common.Models.GameMechanism
{
    public class Move
    {
        public Guid GameId { get; set; }
        public Player Player { get; set; }
        public int Destination { get; set; }
    }
}