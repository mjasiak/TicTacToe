using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using TicTacToe.Server.Handlers;
using TicTacToe.Server.Listeners;
using TicTacToe.Server.Providers;

namespace TicTacToe.Server.Managers
{
    public class TcpServerManager : IServerManager
    {
        private TcpListener _server;
        private bool _isListening = false;
        private readonly ITcpListenerProvider _tcpListenerProvider;
        private readonly IServerListener<TcpListener> _tcpServerListener;

        public TcpServerManager(ITcpListenerProvider tcpListenerProvider, IServerListener<TcpListener> tcpServerListener)
        {
            _tcpListenerProvider = tcpListenerProvider;
            _tcpServerListener = tcpServerListener;
        }
        public void RunServer(string listeningIPAddress, int listeningPort)
        {
            _server = CreateListener(listeningIPAddress, listeningPort);
            StartServer();
            StartListening();
        }

        private void StartListening()
        {
            _tcpServerListener.Listen(_server);
        }

        private TcpListener CreateListener(string listeningIPAddress, int listeningPort)
        {
            IPAddress ipAddressInstance = CreateIPAddressInstance(listeningIPAddress);
            var listener = _tcpListenerProvider.CreateListener(ipAddressInstance, listeningPort);
            return listener;
        }

        private static IPAddress CreateIPAddressInstance(string listeningIPAddress)
        {
            return IPAddress.Parse(listeningIPAddress);
        }

        private void StartServer()
        {
                _server.Start();
                ChangeServerStatus(true);
        }

        private void ChangeServerStatus(bool status)
        {
            _isListening = status;
        }

        public void StopServer()
        {
            _server.Stop();
            ChangeServerStatus(false);
        }

        public bool IsServerListening()
        {
            return _isListening;
        }
    }
}