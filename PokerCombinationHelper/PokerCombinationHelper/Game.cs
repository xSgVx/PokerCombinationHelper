using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;

namespace PokerCombinationHelper
{
    class Game
    {
        public static int GAME_PLAYERS_COUNT { get; set; }
        public static Dictionary<string, List<Card>> PlayersDecks { get; set; } = new Dictionary<string, List<Card>>();
        //public static List<Card> boardDeck;
        public static Dictionary<string, List<Card>> Start()
        {
           //Dictionary<string, List<Card>> playersDecks = new Dictionary<string, List<Card>>();

            for (int i = 0; i < GAME_PLAYERS_COUNT; i++)
            {
                
                string playerName = "Player_" + Convert.ToString(i);

                List<Card> playerCards = Card.GetCards(2);

                PlayersDecks.Add(playerName, playerCards);
            }

            return PlayersDecks;
        }


    }
}
