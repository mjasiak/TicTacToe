using Ninject.Modules;
using TicTacToe.Server.Managers;
using TicTacToe.Server.Providers;

namespace TicTacToe.Server
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ITcpListenerProvider>().To<TcpListenerProvider>();
            Bind<ITcpServerManager>().To<TcpServerManager>();
        }
    }
}