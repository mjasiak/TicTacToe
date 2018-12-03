using TicTacToe.Common.Models;

namespace TicTacToe.Resolver.Managers
{
    public interface IPlayersManager
    {
        ResponseMessage ShowConnectedPlayers();
        ResponseMessage AddPlayer(string data);
    }
}