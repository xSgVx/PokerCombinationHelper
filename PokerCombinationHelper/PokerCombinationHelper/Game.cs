using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerCombinationHelper
{
    class Game
    {
        public static void Start()
        {
            while (true)
            {
                int playersCount = 0;
                Console.WriteLine("Введите количество игроков, от 2 до 10 = ");

                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out playersCount))
                    {
                        if (playersCount < 2 || playersCount > 10)
                        {
                            Console.WriteLine("Неверное число, введите число от 2 до 10 = ");
                        }
                        else break;
                    }
                    else Console.WriteLine("Неверный символ, введите число от 2 до 10 = ");
                }

                Card.Deck = Card.GetDeck();

                var playersList = PlayerCardDistribution(playersCount, 2);

                foreach (var player in playersList)
                {
                    Console.Write($"У игрока {player.PlayerName} в руках карты: ");
                    player.PlayerCards.ForEach(card => Console.Write($"{card.Value.GetDescription()}{card.Suit.GetDescription()}, "));
                    Console.WriteLine();
                }

                var board = BoardCardDistribution(5);

                Console.Write("Карты на столе: ");
                board.BoardCards.ForEach(card => Console.Write($"{card.Value.GetDescription()}{card.Suit.GetDescription()}, "));
                Console.Write($"\nОтсортированный стол: ");
                board.BoardCards.Sort();
                board.BoardCards.ForEach(card => Console.Write($"{card.Value.GetDescription()}{card.Suit.GetDescription()}, "));

                var winner = CombinationChecker.GetWinner(playersList, board.BoardCards);

                if (winner.HandParams.ComboRank == ComboRanks.Draw)
                {
                    Console.WriteLine($"\nНичья, т.к у игроков {winner.PlayerName} одинаковые комбинации {winner.PlayerCards}, начать заново?");
                    Console.WriteLine("Введите N чтобы выйти из программы, или любую кнопку чтобы начать заново");
                    if (Console.ReadLine() == "N") break;
                }
                else
                {
                    Console.Write($"\nПобедитель игрок {winner.PlayerName} c комбинацией {winner.HandParams.ComboRank.GetDescription()}: ");
                    winner.HandParams.Combo.ForEach(card => Console.Write($"{card.Value.GetDescription()}{card.Suit.GetDescription()}, "));
                    Console.WriteLine("\nВведите N чтобы выйти из программы, или любую кнопку чтобы начать заново");
                    if (Console.ReadLine() == "N") break;
                }                
            }
        }

        private static List<Player> PlayerCardDistribution(int playersCount, int cardCount)
        {
            var playerList = new List<Player>();

            for (int i = 0; i < playersCount; i++)
            {
                Player player = new Player
                {
                    PlayerName = "Player_" + Convert.ToString(i),
                    PlayerCards = Card.GetCards(cardCount)
                };

                playerList.Add(player);
            }

            return playerList;
        }

        private static Board BoardCardDistribution(int cardCount)
        {
            Board gameBoard = new Board
            {
                BoardCards = Card.GetCards(cardCount)
            };

            return gameBoard;
        }


    }
}


