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

        public TcpListener CreateListener(string listeningIPAddress, int listeningPort)
        {
            IPAddress ipAddressInstance = CreateIPAddressInstance(listeningIPAddress);
            return new TcpListener(ipAddressInstance, listeningPort);
        }

        private IPAddress CreateIPAddressInstance(string listeningIPAddress)
        {
            return IPAddress.Parse(listeningIPAddress);
        }
    }
}