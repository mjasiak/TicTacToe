namespace TicTacToe.Resolver.Processor
{
    public interface IRequestProcessor
    {
        string GetResponse(string clientRequest);
    }
}