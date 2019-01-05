using System.IO;
using TicTacToe.Common.Models;

namespace TicTacToe.Resolver.Core
{
    public interface IRequestResolver
    {
        ResponseMessage Resolve(string clientRequest);
    }
}