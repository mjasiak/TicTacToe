using System.Net.Sockets;
using Ninject.Modules;
using TicTacToe.Resolver.Converters;
using TicTacToe.Resolver.Core;
using TicTacToe.Resolver.Managers;
using TicTacToe.Server.Handlers;
using TicTacToe.Server.Managers;
using TicTacToe.Server.Providers;

namespace TicTacToe.Server
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ITcpListenerProvider>().To<TcpListenerProvider>();
            Bind<IServerManager<TcpListener>>().To<TcpServerManager>();
            Bind<IConnectionHandler<TcpClient>>().To<TcpConnectionHandler>();
            Bind<IMessageConverter>().To<MessageConverter>();
            Bind<IPlayersManager>().To<PlayersManager>();
            Bind<IRequestResolver>().To<RequestResolver>();
        }
    }
}