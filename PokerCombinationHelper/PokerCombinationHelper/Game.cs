using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerCombinationHelper
{
    class Game
    {
        public static void Start()
        {
            Console.WriteLine("Введите количество игроков = ");
            int playersCount = Convert.ToInt32(Console.ReadLine());

            var playersList = PlayerCardDistribution(playersCount, 2);

            foreach(var player in playersList)
            {
                Console.Write($"У игрока {player.PlayerName} в руках карты: ");
                player.PlayerCards.ForEach(card => Console.Write($"{card.Value.GetDescription()}{card.Suit.GetDescription()}, "));
                Console.WriteLine();
            }

            var board = BoardCardDistribution(5);

            Console.Write("Карты на столе: ");
            board.BoardCards.ForEach(card => Console.Write($"{card.Value.GetDescription()}{card.Suit.GetDescription()}, "));
            Console.WriteLine();
            
            foreach (var player in playersList)
            {
                player.PlayerCards.AddRange(board.BoardCards);
            }

            //playersList.ForEach(player => player.PlayerCards.AddRange(board.BoardCards));

            playersList.ForEach(player => player.HandParams = CombinationChecker.GetPlayerHandParams(player.PlayerCards));

            var winner = CombinationChecker.GetWinner(playersList);

            Console.WriteLine($"Победитель игрок {winner.PlayerName} c комбинацией {winner.HandParams.HandRank.GetDescription()}");
            Console.WriteLine("Рука победителя: ");
            winner.PlayerCards.Sort();
            winner.PlayerCards.ForEach((card) => Console.Write($"{card.Value.GetDescription()}{card.Suit.GetDescription()}, "));

            Console.ReadKey();
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


