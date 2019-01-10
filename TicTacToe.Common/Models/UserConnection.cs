namespace TicTacToe.Common.Models
{
    public class UserConnection
    {
        public UserConnection(string name, ClientConnection connection)
        {
            Name = name;
            Connection = connection;
        }
        public string Name { get; set; }
        public ClientConnection Connection { get; set; }
    }
}