using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TicTacToe.Common.Enums;
using TicTacToe.Common.Models;
using TicTacToe.Common.Models.GameMechanism;
using TicTacToe.Common.Models.Messages;
using TicTacToe.Resolver.Validators;

namespace TicTacToe.Resolver.Processors
{
    public class GameProcessor : IGameProcessor
    {
        private readonly IMoveValidator _moveProcessor;

        public GameProcessor(IMoveValidator moveProcessor)
        {
            _moveProcessor = moveProcessor;
        }
        public ResponseMessage Process(Game game, Move move)
        {
            bool isGameFinished = game.Finished;
            if (!isGameFinished)
            {
                bool isYourTurn = _moveProcessor.IsYourTurn(game.Turn, move.Player.Mark);
                if (isYourTurn)
                {
                    bool isMoveAllowed = _moveProcessor.IsMoveAllowed(game.Map, move.Destination);
                    if (isMoveAllowed)
                    {
                        game.Map[move.Destination] = move.Player.Mark;
                        game.Turn = game.Turn.Equals('X') ? 'O' : 'X';
                        ProcessMap(game);
                        return new ResponseMessage
                        {
                            Data = JsonConvert.SerializeObject(game),
                            Method = "game/moved",
                            Status = MessageStatus.Success,
                        };
                    }
                    else
                    {
                        return new ResponseMessage
                        {
                            Method = "game/moved",
                            Status = MessageStatus.Failure,
                            Text = "You're not allowed to move here."
                        };
                    }
                }
                else
                {
                    return new ResponseMessage
                    {
                        Method = "game/moved",
                        Status = MessageStatus.Failure,
                        Text = "It's not your turn. Wait for your opponent."
                    };
                }
            }
            else
            {
                return new ResponseMessage
                {
                    Method = "game/moved",
                    Status = MessageStatus.Failure,
                    Text = "Game finished. If you want to start over, please write 'newgame'."
                };
            }
        }

        private void ProcessMap(Game game)
        {
            var rowsCheckResult = CheckRows(game.Map);
            var columnsCheckResult = CheckColumns(game.Map);
            var crossesCheckResult = CheckCrosses(game.Map);

            ValidateResult(game, rowsCheckResult);
            ValidateResult(game, columnsCheckResult);
            ValidateResult(game, crossesCheckResult);
        }
        private char CheckRows(char[] map)
        {
            for (int i = 0; i < map.Length; i += 3)
            {
                if (map[i] == map[i + 1] && map[i + 1] == map[i + 2])
                    return map[i];
            }
            return 'N';
        }
        private char CheckColumns(char[] map)
        {
            for (int i = 0; i < 3; i++)
            {
                if (map[i] == map[i + 3] && map[i + 3] == map[i + 6])
                    return map[i];
            }
            return 'N';
        }
        private char CheckCrosses(char[] map)
        {
            if (map[0] == map[4] && map[4] == map[8])
            {
                return map[0];
            }
            if (map[2] == map[4] && map[4] == map[6])
            {
                return map[2];
            }
            return 'N';
        }
        private void ValidateResult(Game game, char checkResult)
        {
            if (!checkResult.Equals('N'))
            {
                game.Winner = checkResult;
            }
        }
    }

    public interface IGameProcessor
    {
        ResponseMessage Process(Game game, Move move);
    }
}