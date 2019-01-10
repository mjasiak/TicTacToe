using TicTacToe.Common.Models.Messages;

namespace TicTacToe.Resolver.Managers
{
    public interface IPlayersManager
    {
        ResponseMessage ShowConnectedPlayers();
        ResponseMessage AddPlayer(string playerToAddData);
        ResponseMessage RemovePlayer(string playerToRemoveData);
    }
}