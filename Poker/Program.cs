using Poker.Models;
using Poker.Source;

internal class Program
{
    private static void Main(string[] args)
    {
        ConsoleKeyInfo responce;
        var poker = new PokerGame();
        poker.MessagesHandler += Helpers.DisplayMessage;

        while (true)
        {
            poker.SetPlayersCount(Helpers.GetNumberOfPlayersFromConsole());
            poker.StartGame();

            while (true)
            {
                responce = Helpers.GetConsoleResponce(Messages.NextActionButton);

                if (responce.Key == ConsoleKey.A)
                {
                    poker.AddCardToBoard();
                }

                if (responce.Key == ConsoleKey.R)
                {
                    break;
                }

                if (responce.Key == ConsoleKey.Escape)
                {
                    return;
                }
                else
                {
                    Helpers.DisplayMessage(Messages.InputError);
                }
            }
        }
    }
}