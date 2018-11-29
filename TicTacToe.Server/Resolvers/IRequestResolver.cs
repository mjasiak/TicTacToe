using System.IO;
using TicTacToe.Common.Models;

namespace TicTacToe.Server.Resolvers
{
    public interface IRequestResolver
    {
        void Resolve(Stream clientStream);
    }
}