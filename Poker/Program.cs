using CardGameBase.Factories;
using Poker.Models;
internal class Program
{
    private static void Main(string[] args)
    {
        var poker = new PokerGame();

        poker.MessagesHandler += DisplayMessage;
        poker.SetPlayersCount(5);
        poker.StartGame();



    }
    static void DisplayMessage(string message) => Console.WriteLine(message);
}