using System.Net;
using System.Net.Sockets;

namespace TicTacToe.Server.Providers
{
    public class TcpListenerProvider : ITcpListenerProvider
    {
        public TcpListener CreateListener(IPAddress listeningIPAddress, int listeningPort)
        {
            return new TcpListener(listeningIPAddress, listeningPort);
        }
    }
}