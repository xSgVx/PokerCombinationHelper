using System;
using System.Collections.Generic;

namespace PokerCombinationHelper
{
    class Game
    {
        public static int GAME_PLAYERS_COUNT;

        public static void Start()
        {

            var playersList = PlayerCardDistribution(2);

            var board = BoardCardDistribution(5);

            var cardList = new List<Card>();

            playersList.ForEach(player => player.PlayerDeck.ForEach(card => cardList.Add(card)));

            board.BoardCards.ForEach(card => cardList.Add(card));

            playersList.ForEach(player => player.HandRank = CombinationChecker.GetPlayerHandRank(cardList).HandRank);

        }

        private static List<Player> PlayerCardDistribution(int cardCount)
        {
            var playerList = new List<Player>();

            for (int i = 0; i < GAME_PLAYERS_COUNT; i++)
            {
                Player player = new Player
                {
                    PlayerName = "Player_" + Convert.ToString(i),

                    PlayerDeck = Card.GetCards(cardCount)
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


