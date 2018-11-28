using System.IO;
using System.Net;
using System.Net.Sockets;
using TicTacToe.Server.Providers;

namespace TicTacToe.Server.Managers {
    public class TcpServerManager : ITcpServerManager {
        public void Start(TcpListener server)
        {
            server.Start();
        }
        public void Stop(TcpListener server) {
            server.Stop();
        }

        public void Listen(TcpListener server) {
            while (true) {
                TcpClient client = server.AcceptTcpClient();
                var clientStream = client.GetStream();
                using(var streamReader = new StreamReader(clientStream)){
                    var clientRequest = streamReader.ReadLine();
                }
            }
        }
    }
}