namespace Poker.Source
{
    internal static class Helpers
    {
        internal static void DisplayMessage(string message) => Console.WriteLine(message);

        internal static string CollectionToString<T>(IEnumerable<T> collection, string separator = "\n")
        {
            return String.Join(separator, collection.Select(x => x?.ToString()));
        }

        internal static byte GetNumberOfPlayersFromConsole()
        {
            while (true)
            {
                var responce = GetConsoleResponce(Messages.EnterPlayersCount);

                if (char.IsNumber(responce.KeyChar))
                {
                    var value = (byte)char.GetNumericValue(responce.KeyChar);

                    if (value >= 2 && value <= 6)
                        return value;
                    else
                    {
                        DisplayMessage(Messages.InputError);
                    }
                }
                else
                {
                    DisplayMessage(Messages.InputError);
                }
            }
        }

        internal static ConsoleKeyInfo GetConsoleResponce(string outputMessage)
        {
            DisplayMessage(outputMessage);
            var responce = Console.ReadKey();
            Console.WriteLine();

            return responce;
        }
    }
}
