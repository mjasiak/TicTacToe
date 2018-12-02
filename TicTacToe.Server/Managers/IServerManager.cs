using System.Net;
using System.Net.Sockets;

namespace TicTacToe.Server.Managers
{
    public interface IServerManager
    {
        void RunServer(string listeningIPAddress,int listeningPort);
        void StopServer();
        bool IsServerListening();
    }
}