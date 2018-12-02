namespace TicTacToe.Server.Listeners
{
    public interface IServerListener<T>
    {
        void Listen(T server);
    }
}