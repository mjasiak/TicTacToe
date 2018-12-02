using System.Net.Sockets;
using Ninject.Modules;
using TicTacToe.Server.Handlers;
using TicTacToe.Server.Managers;
using TicTacToe.Server.Providers;
using TicTacToe.Server.Resolvers;

namespace TicTacToe.Server
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ITcpListenerProvider>().To<TcpListenerProvider>();
            Bind<IServerManager>().To<TcpServerManager>();
            Bind<IConnectionHandler<TcpListener>>().To<TcpConnectionHandler>();
            Bind<IRequestResolver>().To<RequestResolver>();
        }
    }
}