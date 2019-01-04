using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Common;

namespace TicTacToe.Tester
{
    class Program
    {
        private static string _listeningAddress = "127.0.0.1";
        private static int _listeningPort = 50000;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome in TicTacToe v.0.1 by Michal Jasiak...");
            Console.WriteLine("Please give your name...");
            var name = Console.ReadLine();
            Console.WriteLine(string.Format("Hello {0}...", name));
            Console.WriteLine("Now we will try to connect you to the game host...");

            var client = new ClientConnection();
            client.OnMessageReceived += connectionManager_OnMessageReceived;
            client.Connect(_listeningAddress, _listeningPort);
            client.Send(name);
            client.StartListening();

            string message = string.Empty;
            message = Console.ReadLine();
            while (!message.Equals("exit"))
            {
                client.Send(message);
                message = Console.ReadLine();
            }
            // var clientConnection = new ClientConnection();
            // clientConnection.Connect(_listeningAddress, _listeningPort);
            // // var tcpClient = new TcpClient(_listeningAddress, _listeningPort);

            // // Console.WriteLine($"Connected with server on {_listeningAddress}:{_listeningPort}");
            // // Introduce(name, tcpClient);
            // // // var positions = InitPositions();
            // // // BuildMap(positions);
            // // // clientConnection.OnMessageReceived += HandleReceviedMessage;
            // // // clientConnection.Receive();
            // // _listeningTask = Task.Run(() =>
            // // {
            // //     var stream = tcpClient.GetStream();
            // //     Console.WriteLine("* You have started listening... *");
            // //     while (true)
            // //     {
            // //         var streamReader = new StreamReader(stream);
            // //         var response = streamReader.ReadLine();
            // //         Console.WriteLine(response);
            // //     }
            // // });
            // // string message = string.Empty;
            // // message = Console.ReadLine();
            // // while (!message.Equals("exit"))
            // // {
            // //     var clientStream = tcpClient.GetStream();
            // //     var streamWriter = new StreamWriter(clientStream);
            // //     streamWriter.WriteLine(message);
            // //     streamWriter.Flush();
            // //     message = Console.ReadLine();
            // // }
            // while (!message.Equals("exit"))
            // {
            //     var client = new TcpClient(_listeningAddress, _listeningPort);
            //     var stream = client.GetStream();
            //     var streamWriter = new StreamWriter(stream);
            //     streamWriter.Write(message);
            //     streamWriter.Flush();
            //     var response = GetClientRequest(stream);
            //     Console.WriteLine($"Response: {response}");
            //     Console.WriteLine("New command: ");
            //     message = Console.ReadLine();
            // }
            Console.WriteLine("* Client has exited... *");
        }

        private static void connectionManager_OnMessageReceived(object source, string message)
        {
            Console.WriteLine(message);
        }

        #region NotUsed
        private static void Introduce(string name, TcpClient tcpClient)
        {
            var stream = tcpClient.GetStream();
            var streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine(name);
            streamWriter.Flush();
        }

        static void BuildMap(string[] positions)
        {
            Console.WriteLine($" {positions[0]}  |  {positions[1]}  |  {positions[2]}  ");
            Console.WriteLine("----------------------");
            Console.WriteLine($" {positions[3]}  |  {positions[4]}  |  {positions[5]}  ");
            Console.WriteLine("----------------------");
            Console.WriteLine($" {positions[6]}  |  {positions[7]}  |  {positions[8]}  ");
        }
        static string[] InitPositions()
        {
            var positions = new string[9];
            for (int i = 0; i < positions.Length; i++)
            {
                if (i % 2 == 0)
                {
                    positions[i] = "X";
                }
                else
                {
                    positions[i] = " ";
                }
            }
            return positions;
        }
        private static string GetClientRequest(NetworkStream clientStream)
        {
            using (var catchedStream = new MemoryStream())
            {
                byte[] data = new byte[1024];
                do
                {
                    clientStream.Read(data, 0, data.Length);
                    catchedStream.Write(data, 0, data.Length);
                } while (clientStream.DataAvailable);
                return Encoding.ASCII.GetString(catchedStream.GetBuffer(), 0, (int)catchedStream.Length);
            }
        }
        #endregion
    }
}
