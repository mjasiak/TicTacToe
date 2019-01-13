using System.Linq;
using Newtonsoft.Json;
using TicTacToe.Common.Enums;
using TicTacToe.Common.Models;
using TicTacToe.Common.Models.GameMechanism;
using TicTacToe.Common.Models.Messages;

namespace TicTacToe.Resolver.Validators
{
    public class MoveValidator : IMoveValidator
    {
        public bool IsMoveAllowed(char[] map, int destination)
        {
            return  map[destination] != 'X' && map[destination] != 'O' ?
                    true : false;
        }

        public bool IsYourTurn(char gameTurn, char playerMark)
        {
            return gameTurn == playerMark ? true : false;
        }
    }

    public interface IMoveValidator
    {
        bool IsYourTurn(char gameTurn, char playerMark);
        bool IsMoveAllowed(char[] map, int destination);
    }
}