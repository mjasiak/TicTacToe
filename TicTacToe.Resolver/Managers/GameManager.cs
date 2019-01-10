using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TicTacToe.Common.Enums;
using TicTacToe.Common.Extensions;
using TicTacToe.Common.Models;
using TicTacToe.Common.Models.GameMechanism;
using TicTacToe.Common.Models.Messages;
using TicTacToe.Resolver.Processors;

namespace TicTacToe.Resolver.Managers
{
    public class GameManager : IGameManager
    {
        private readonly IPlayersManager _playersManager;
        private readonly IMoveProcessor _moveProcessor;
        private List<Game> _activeGames = new List<Game>();

        public GameManager(IPlayersManager playersManager, IMoveProcessor moveProcessor)
        {
            _playersManager = playersManager;
            _moveProcessor = moveProcessor;
        }
        public ResponseMessage Start()
        {
            List<Player> players = GetPlayers();
            if (players.Count > 1)
            {
                var newGame = new Game(Guid.NewGuid())
                {
                    Players = players,
                    Map = InitializeMap()
                };
                _activeGames.Add(newGame);
                return new ResponseMessage
                {
                    Status = MessageStatus.Success,
                    Data = JsonConvert.SerializeObject(newGame),
                    Method = "game/started",
                    Text = "* Game has been started *"
                };
            }
            else
            {
                return new ResponseMessage
                {
                    Status = MessageStatus.Failure,
                    Data = string.Empty,
                    Method = "game/started",
                    Text = "There is no other player to start game, you have to wait."
                };
            }
        }

        private List<Player> GetPlayers()
        {
            var managerResponse = _playersManager.ShowConnectedPlayers();
            return managerResponse.Data.Deserialize<List<Player>>();
        }
        private char[] InitializeMap()
        {
            var positions = new char[9];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = char.Parse($"{i + 1}");
            }
            return positions;
        }

        public ResponseMessage Move(string moveData)
        {
            Move move = moveData.Deserialize<Move>();
            Game game = _activeGames.FirstOrDefault(g => g.Id == move.GameId);            
            return _moveProcessor.Process(game, move);
        }
    }

    public interface IGameManager
    {
        ResponseMessage Start();
        ResponseMessage Move(string moveData);
    }
}