using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TicTacToe.Common
{
    public class ClientConnectionHandler : IClientConnectionHandler
    {
        private NetworkStream _networkStream;
        private Task _listeningTask;

        public ClientConnectionHandler(ClientConnection connection)
        {
            Connection = connection;
            _networkStream = Connection.Socket.GetStream();
        }

        public ClientConnection Connection { get; private set; }

        public delegate void MessageReceivedHandler(object source, string message);
        public event MessageReceivedHandler OnMessageReceived;

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
            Connection.Socket.Close();
        }

        public void StartListening()
        {
            _listeningTask = Task.Run(() =>
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

    public interface IClientConnectionHandler
    {
        void Close();
        string Receive();
        void Send(string data);
        void StartListening();
    }
}