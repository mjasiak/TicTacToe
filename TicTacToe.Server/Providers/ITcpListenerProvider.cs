using System.Net;
using System.Net.Sockets;

namespace TicTacToe.Server.Providers
{
    public interface ITcpListenerProvider
    {
        TcpListener CreateListener(IPAddress listeningIPAddress, int listeningPort);
        TcpListener CreateListener(string listeningIPAddress, int listeningPort);
    }
}