using System.Collections.Generic;
using Newtonsoft.Json;
using TicTacToe.Common.Enums;
using TicTacToe.Common.Models;

namespace TicTacToe.Resolver.Managers
{
    public class GameManager : IGameManager
    {
        private readonly IPlayersManager _playersManager;

        public GameManager(IPlayersManager playersManager)
        {
            _playersManager = playersManager;
        }
        public ResponseMessage Start()
        {
            var players = GetPlayers();
            if (players.Count > 1)
            {
                var newGame = new Game
                {
                    Players = players,
                    Map = InitializeMap()
                };
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
            var serializedPlayers = managerResponse.Data;
            return JsonConvert.DeserializeObject<List<Player>>(serializedPlayers);
        }
        private string[] InitializeMap()
        {
            var positions = new string[9];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = $"{i+1}";
            }
            return positions;
        }
    }

    public interface IGameManager
    {
        ResponseMessage Start();
    }
}