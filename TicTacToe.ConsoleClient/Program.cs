using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TicTacToe.Common;
using TicTacToe.Common.Converters;
using TicTacToe.Common.Extensions;
using TicTacToe.Common.Enums;
using TicTacToe.Common.Models;
using TicTacToe.Common.Models.Messages;
using TicTacToe.Common.Models.GameMechanism;
using System.Linq;

namespace TicTacToe.ConsoleClient
{
    class Program
    {
        private static string _listeningAddress = "127.0.0.1";
        private static int _listeningPort = 50000;
        private static IMessageConverter _messageConverter;
        private static Player _currentPlayer;
        private static Game _currentGame;

        static Program()
        {
            _messageConverter = new MessageConverter();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome in Console.TicTacToe v.0.1 by Michal Jasiak...\r\n");
            Console.WriteLine("Please give your name...");
            var name = Console.ReadLine();
            Console.WriteLine(string.Format("Hello {0},\r\ndo you want X or O?", name));
            var mark = Console.ReadLine();
            Console.WriteLine("Connecting...");

            var connection = ClientConnection.Connect(_listeningAddress, _listeningPort);
            var connectionHandler = new ClientConnectionHandler(connection);
            connectionHandler.OnMessageReceived += connectionHandler_OnMessageReceived;
            var requestMessage = new RequestMessage
            {
                Data = JsonConvert.SerializeObject(new Player
                {
                    Name = name,
                    Mark = char.Parse(mark)
                }),
                Method = "player/add"
            };
            connectionHandler.Send(JsonConvert.SerializeObject(requestMessage));
            connectionHandler.StartListening();

            string message = string.Empty;
            message = Console.ReadLine();
            while (!message.Equals("exit"))
            {
                var choose = message.Split(' ')[0];
                Console.WriteLine(choose);
                switch (choose)
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
                    case "refresh":
                        {
                            Console.Clear();
                            BuildMap(_currentGame.Map);
                            break;
                        }
                    case "move":
                        {
                            var isNumeric = int.TryParse(message.Split(' ')[1], out int destination);
                            if (isNumeric)
                            {
                                bool isNumber1to9 = Enumerable.Range(1, 9).Contains(destination);
                                if (isNumber1to9)
                                {
                                    Move move = new Move
                                    {
                                        GameId = _currentGame.Id,
                                        Player = _currentPlayer,
                                        Destination = destination
                                    };
                                    requestMessage = new RequestMessage
                                    {
                                        Data = JsonConvert.SerializeObject(move),
                                        Method = "game/move"
                                    };
                                    connectionHandler.Send(requestMessage.Serialize());
                                    break;
                                }
                            }
                            BuildMap(_currentGame.Map);
                            Console.WriteLine("First parameter of 'move' should be number from 1-9");
                            break;
                        }
                    case "help":
                        {
                            Console.Clear();
                            Console.WriteLine("--- TicTacToe Help Menu ---");
                            Console.WriteLine("  - newgame | Starts new game.");
                            Console.WriteLine("  - refresh | Refresh game map.");
                            Console.WriteLine("  - help    | Shows help menu");
                            Console.WriteLine("  - exit    | Close game.");
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
                        if (responseMessage.Status == MessageStatus.Failure)
                        {
                            Console.WriteLine(responseMessage.Text);
                            if (responseMessage.InnerMethod.Equals("samechar"))
                            {
                                var player = responseMessage.Data.Deserialize<Player>();
                                JsonConvert.DeserializeObject<Player>(responseMessage.Data);
                                var requestMessage = new RequestMessage
                                {
                                    Data = JsonConvert.SerializeObject(new Player
                                    {
                                        Name = player.Name,
                                        Mark = player.Mark.Equals('X') ? 'O' : 'X'
                                    }),
                                    Method = "player/add"
                                };
                                clientConnectionHandler.Send(_messageConverter.ConvertToJson(requestMessage));
                            }
                        }
                        if (responseMessage.Status == MessageStatus.Success)
                        {
                            if (!string.IsNullOrEmpty(responseMessage.Data))
                            {
                                _currentPlayer = responseMessage.Data.Deserialize<Player>();
                                Console.WriteLine("* Connected successfully *");
                            }
                            else
                            {
                                Console.WriteLine(responseMessage.Text);
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
                            _currentGame = responseMessage.Data.Deserialize<Game>();
                            BuildMap(_currentGame.Map);
                            if (_currentPlayer.Mark.Equals('X'))
                            {
                                Console.WriteLine("* You start *");
                            }
                            else
                            {
                                Console.WriteLine("* Your enemy starts *");
                            }
                        }
                        else
                        {
                            Console.WriteLine(responseMessage.Text);
                        }
                        break;
                    }
                case "game/moved":
                    {
                        if (responseMessage.Status == MessageStatus.Success)
                        {
                            _currentGame = responseMessage.Data.Deserialize<Game>();
                            Console.Clear();
                            BuildMap(_currentGame.Map);
                            bool isYourTurn = _currentGame.Turn == _currentPlayer.Mark;
                            if (isYourTurn)
                            {
                                Console.WriteLine("* Your turn *");
                            }
                            else
                            {
                                Console.WriteLine("* Your opponent turn *");
                            }
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

        static void BuildMap(char[] map)
        {
            Console.WriteLine($" {map[0]}  |  {map[1]}  |  {map[2]}  ");
            Console.WriteLine("---------------");
            Console.WriteLine($" {map[3]}  |  {map[4]}  |  {map[5]}  ");
            Console.WriteLine("---------------");
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
