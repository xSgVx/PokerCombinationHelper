using System;

namespace PokerCombinationHelper
{
    class Game
    {
        public static int GAME_PLAYERS_COUNT;

        public static void FirstPlayerCardDistribution()
        {
            for (int i = 0; i < GAME_PLAYERS_COUNT; i++)
            {
                Player player = new Player
                {
                    PlayerName = "Player_" + Convert.ToString(i),

                    PlayerDeck = Card.GetCards(2)
                };
            }     
        }

        public static void FirstBoardCardDistribution()
        {
            Board gameBoard = new Board();

            gameBoard.BoardCards = Card.GetCards(3);
        }


    }
}


