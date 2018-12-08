using System.Collections.Generic;
using Newtonsoft.Json;
using TicTacToe.Common.Enums;
using TicTacToe.Common.Models;

namespace TicTacToe.Resolver.Managers {
    public class PlayersManager : IPlayersManager
    {
        private List<Player> _listOfConnectedPlayers = new List<Player>(){
            new Player{
                Name="Jasiek"
            }
        };
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