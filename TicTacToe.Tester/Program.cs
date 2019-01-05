using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TicTacToe.Common;
using TicTacToe.Common.Converters;
using TicTacToe.Common.Enums;
using TicTacToe.Common.Models;

namespace TicTacToe.Tester
{
    class Program
    {
        private static string _listeningAddress = "127.0.0.1";
        private static int _listeningPort = 50000;
        private static IMessageConverter _messageConverter;

        static Program()
        {
            _messageConverter = new MessageConverter();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome in Console.TicTacToe v.0.1 by Michal Jasiak...\r\n");
            Console.WriteLine("Please give your name...");
            var name = Console.ReadLine();
            Console.WriteLine(string.Format("Hello {0} do you want X or O?", name));
            var flag = Console.ReadLine();
            Console.WriteLine("Connecting...");

            var connection = ClientConnection.Connect(_listeningAddress, _listeningPort);
            var connectionHandler = new ClientConnectionHandler(connection);
            connectionHandler.OnMessageReceived += connectionHandler_OnMessageReceived;
            var requestMessage = new RequestMessage
            {
                Data = JsonConvert.SerializeObject(new Player
                {
                    Name = name,
                    XO = flag
                }),
                Method = "player/add"
            };
            connectionHandler.Send(JsonConvert.SerializeObject(requestMessage));
            connectionHandler.StartListening();

            string message = string.Empty;
            message = Console.ReadLine();
            while (!message.Equals("exit"))
            {
                switch (message)
                {
                    case "newgame":
                        {
                            requestMessage = new RequestMessage
                            {
                                Data = string.Empty,
                                Method = "game/start"
                            };
                            connectionHandler.Send(requestMessage.Serialize());
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Command doesn't exist");
                            break;
                        }
                }
                message = Console.ReadLine();
            }

            Console.WriteLine("* Client has exited... *");
            Console.Clear();
        }

        private static void connectionHandler_OnMessageReceived(object source, string message)
        {
            var clientConnectionHandler = source as ClientConnectionHandler;
            var responseMessage = _messageConverter.ConvertToResponseMessage(message);
            switch (responseMessage.Method)
            {
                case "player/added":
                    {
                        Console.WriteLine(responseMessage.Text);
                        if (responseMessage.Status == MessageStatus.Failure)
                        {
                            if (responseMessage.InnerMethod.Equals("samechar"))
                            {
                                var player = JsonConvert.DeserializeObject<Player>(responseMessage.Data);
                                var requestMessage = new RequestMessage
                                {
                                    Data = JsonConvert.SerializeObject(new Player
                                    {
                                        Name = player.Name,
                                        XO = player.XO.Equals("X") ? "O" : "X"
                                    }),
                                    Method = "player/add"
                                };
                                clientConnectionHandler.Send(_messageConverter.ConvertToJson(requestMessage));
                            }
                        }
                        break;
                    }
                case "game/started":
                    {
                        if (responseMessage.Status == MessageStatus.Success)
                        {
                            Console.Clear();
                            Console.WriteLine("* Game has been started *\r\n");
                            Game game = JsonConvert.DeserializeObject<Game>(responseMessage.Data);
                            BuildMap(game.Map);
                        }
                        else
                        {
                            Console.WriteLine(responseMessage.Text);
                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("* Something bad happened *");
                        break;
                    }
            }
        }

        #region NotUsed
        private static void Introduce(string name, TcpClient tcpClient)
        {
            var stream = tcpClient.GetStream();
            var streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine(name);
            streamWriter.Flush();
        }

        static void BuildMap(string[] map)
        {
            Console.WriteLine($" {map[0]}  |  {map[1]}  |  {map[2]}  ");
            Console.WriteLine("----------------------");
            Console.WriteLine($" {map[3]}  |  {map[4]}  |  {map[5]}  ");
            Console.WriteLine("----------------------");
            Console.WriteLine($" {map[6]}  |  {map[7]}  |  {map[8]}  ");
        }
        static string[] InitPositions()
        {
            var positions = new string[9];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = " ";
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
