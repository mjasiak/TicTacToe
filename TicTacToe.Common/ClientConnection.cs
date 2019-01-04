using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TicTacToe.Common
{
    public class ClientConnection
    {
        public delegate void MessageReceivedHandler(object source, string message);
        public event MessageReceivedHandler OnMessageReceived;
        public TcpClient Socket {get; private set;}
        public readonly Guid Id;
        private NetworkStream _networkStream;

        private Task _listeningTask;

        public ClientConnection() { 
            Id = Guid.NewGuid();
            Socket = new TcpClient();
        }
        public ClientConnection(TcpClient clientSocket)
        {
            Id = Guid.NewGuid();
            Socket = clientSocket;
            _networkStream = Socket.GetStream();
        }

        public void Connect(string ipAddress, int port)
        {
            Socket.Connect(ipAddress, port);
            if (Socket.Connected)
            {
                _networkStream = Socket.GetStream();
            }
        }

        public void Send(string data)
        {
            var streamWriter = new StreamWriter(_networkStream);
            streamWriter.WriteLine(data);
            streamWriter.Flush();
        }

        public string Receive()
        {
            var stringReader = new StreamReader(_networkStream);
            return stringReader.ReadLine();
        }

        public void Close()
        {
            Socket.Close();
        }

        public void StartListening()
        {
            this._listeningTask = Task.Run(() =>
            {
                while (true)
                {
                    var streamReader = new StreamReader(_networkStream);
                    var response = streamReader.ReadLine();
                    OnMessageReceived(this, response);
                }
            });
        }
    }
}