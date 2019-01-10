using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TicTacToe.Common.Enums;
using TicTacToe.Common.Models;
using TicTacToe.Common.Models.Messages;

namespace TicTacToe.Resolver.Managers
{
    public class PlayersManager : IPlayersManager
    {
        private List<Player> _listOfConnectedPlayers = new List<Player>() { };
        public ResponseMessage AddPlayer(string data)
        {
            var newPlayer = JsonConvert.DeserializeObject<Player>(data);
            var sameChar = _listOfConnectedPlayers.Any(p => p.Mark == newPlayer.Mark);
            if (sameChar)
            {
                return new ResponseMessage
                {
                    Data = JsonConvert.SerializeObject(newPlayer),
                    Method = "player/added",
                    InnerMethod = "samechar",
                    Status = MessageStatus.Failure,
                    Text = string.Format("Your opponent choosed {0} first, you'll start with {1}", newPlayer.Mark, newPlayer.Mark.Equals('X') ? "O" : "X")
                };
            }
            else
            {
                _listOfConnectedPlayers.Add(newPlayer);
                return new ResponseMessage
                {
                    Data = JsonConvert.SerializeObject(newPlayer),
                    Method = "player/added",
                    Status = MessageStatus.Success,
                    Hidden = true,
                    Text = $"Player {newPlayer.Name} connected succesfully."
                };
            }
        }

        public ResponseMessage RemovePlayer(string playerToRemoveData)
        {
            var playerToRemove = JsonConvert.DeserializeObject<Player>(playerToRemoveData);
            _listOfConnectedPlayers.Remove(playerToRemove);

            return new ResponseMessage
            {
                Status = MessageStatus.Success,
                Data = string.Empty,
                Text = "Player successfully removed"
            };
        }

        public ResponseMessage ShowConnectedPlayers()
        {
            return new ResponseMessage
            {
                Status = MessageStatus.Success,
                Data = JsonConvert.SerializeObject(_listOfConnectedPlayers),
                Text = string.Empty
            };
        }
    }
}