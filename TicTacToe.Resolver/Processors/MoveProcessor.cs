using System.Linq;
using Newtonsoft.Json;
using TicTacToe.Common.Enums;
using TicTacToe.Common.Models;
using TicTacToe.Common.Models.GameMechanism;
using TicTacToe.Common.Models.Messages;

namespace TicTacToe.Resolver.Processors
{
    public class MoveProcessor : IMoveProcessor
    {
        public ResponseMessage Process(Game game, Move move)
        {
            bool areYourTurn = game.Turn == move.Player.Mark ? true : false;
            if (areYourTurn)
            {
                bool isMoveAllowed =
                    game.Map[move.Destination] != 'X' && game.Map[move.Destination] != 'O' ?
                    true : false;
                    
                if (isMoveAllowed)
                {
                    game.Map[move.Destination] = move.Player.Mark;
                    game.Turn = game.Turn.Equals('X') ? 'O' : 'X';
                    return new ResponseMessage
                    {
                        Data = JsonConvert.SerializeObject(game),
                        Method = "game/moved",
                        Status = MessageStatus.Success,
                    };
                }
            }

            throw new System.NotImplementedException();
        }
    }

    public interface IMoveProcessor
    {
        ResponseMessage Process(Game game, Move move);
    }
}