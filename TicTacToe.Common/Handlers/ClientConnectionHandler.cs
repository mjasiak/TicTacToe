using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace TicTacToe.Common
{
    public class ClientConnectionHandler : IClientConnectionHandler
    {
        private NetworkStream _networkStream;
        private Task _listeningTask;
        private readonly CancellationTokenSource _cancelationTokenSource;

        public ClientConnectionHandler(ClientConnection connection)
        {
            Connection = connection;
            _networkStream = Connection.Socket.GetStream();
            _cancelationTokenSource = new CancellationTokenSource();
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
            _cancelationTokenSource.Cancel();
            Connection.Socket.Client.Disconnect(false);
        }

        public void StartListening()
        {
            var ct = _cancelationTokenSource.Token;
            _listeningTask = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    var streamReader = new StreamReader(_networkStream);
                    try
                    {
                        var response = streamReader.ReadLine();
                        OnMessageReceived(this, response);
                    }
                    catch
                    {
                        if (ct.IsCancellationRequested)
                        {
                            break;
                        }
                    }
                }
            }, ct);
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