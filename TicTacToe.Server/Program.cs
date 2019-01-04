using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using TicTacToe.Common;
using TicTacToe.Server.Managers;
using TicTacToe.Server.Providers;

namespace TicTacToe.Server
{
    class Program
    {
        private const string _listeningAddress = "127.0.0.1";
        private const int _listeningPort = 50000;
        private static readonly IServerManager<TcpListener> _serverManager;
        private static readonly ITcpListenerProvider _tcpListenerProvider;
        static Program()
        {
            var kernel = new StandardKernel(new Bindings());
            _serverManager = kernel.Get<IServerManager<TcpListener>>();
            _tcpListenerProvider = kernel.Get<ITcpListenerProvider>();
        }
        static void Main(string[] args)
        {
            var server = CreateListener(_listeningAddress, _listeningPort);
            server.Start();
            ListenToClients(server);
            Console.WriteLine("Server stopped running. Press any key to close.");
            Console.Read();
        }

        static List<UserConnectionModel> _connectedUsers = new List<UserConnectionModel>();
        static void ListenToClients(TcpListener server)
        {
            while (true)
            {
                var client = server.AcceptTcpClient();

                // WORKING BELOW
                // var nameStream = client.GetStream();
                // var streamReader = new StreamReader(nameStream);
                // var name = streamReader.ReadLine();

                // _connectedUsers.Add(new ConnectionModel(name, client));

                // Task.Run(() => { HandleClientConnection(client); });

                //DOESN'T WORK
                var connection = new ClientConnection(client);
                var name = connection.Receive();

                _connectedUsers.Add(new UserConnectionModel(name, connection));

                connection.OnMessageReceived += connectionManager_OnMessageReceived;
                connection.StartListening();
            }
        }

        private static void connectionManager_OnMessageReceived(object source, string message)
        {
            var client = source as ClientConnection;

            Console.WriteLine(string.Format("Server got message: {0}", message));
            if (_connectedUsers.Count <= 1)
            {
                client.Send("You're only one connected client.");
            }
            else
            {
                foreach (var user in _connectedUsers)
                {
                    if (user.Connection.Id != client.Id)
                    {
                        user.Connection.Send(string.Format("{0}: {1}", user.Name, message));
                    }
                }
            }
        }


        private static TcpListener CreateListener(string listeningIPAddress, int listeningPort)
        {
            return _tcpListenerProvider.CreateListener(listeningIPAddress, listeningPort);
        }
    }

    public class UserConnectionModel
    {
        public UserConnectionModel(string name, ClientConnection connection)
        {
            Name = name;
            Connection = connection;
        }
        public string Name { get; set; }
        public ClientConnection Connection { get; set; }
    }
}