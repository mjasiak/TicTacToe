using System.Net.Sockets;
using Ninject.Modules;
using TicTacToe.Common.Converters;
using TicTacToe.Resolver.Core;
using TicTacToe.Resolver.Managers;
using TicTacToe.Resolver.Processors;
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
            Bind<IPlayersManager>().To<PlayersManager>().InSingletonScope();
            Bind<IGameManager>().To<GameManager>().InSingletonScope();
            Bind<IRequestResolver>().To<RequestResolver>();
            Bind<IMoveProcessor>().To<MoveProcessor>().InSingletonScope();
        }
    }
}