using TicTacToe.Common.Models.Messages;

namespace TicTacToe.Resolver.Core
{
    public interface IRequestResolver
    {
        ResponseMessage Resolve(string clientRequest);
    }
}