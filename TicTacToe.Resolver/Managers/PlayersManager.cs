using System.Collections.Generic;
using Newtonsoft.Json;
using TicTacToe.Common.Enums;
using TicTacToe.Common.Models;

namespace TicTacToe.Resolver.Managers {
    public class PlayersManager : IPlayersManager
    {
        private List<Player> _listOfConnectedPlayers = new List<Player>(){};
        public ResponseMessage AddPlayer(string data)
        {
            var newPlayer = JsonConvert.DeserializeObject<Player>(data);
            _listOfConnectedPlayers.Add(newPlayer);
            return new ResponseMessage{
                Status = MessageStatus.Success,
                Data = string.Empty,
                Text = "New player successfully added"
            };
        }

        public ResponseMessage RemovePlayer(string playerToRemoveData)
        {
            var playerToRemove = JsonConvert.DeserializeObject<Player>(playerToRemoveData);
            _listOfConnectedPlayers.Remove(playerToRemove);

            return new ResponseMessage {
                Status = MessageStatus.Success,
                Data = string.Empty,
                Text = "Player successfully removed"
            };
        }

        public ResponseMessage ShowConnectedPlayers()
        {
            return new ResponseMessage{
                Status = MessageStatus.Success,
                Data = JsonConvert.SerializeObject(_listOfConnectedPlayers),
                Text = string.Empty
            };
        }
    }
}