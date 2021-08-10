using System.Collections.Generic;

namespace PokerCombinationHelper
{
    public class Player
    {
        public string PlayerName { get; set; }
        public List<Card> PlayerDeck { get; set; }
        public PokerHandRankings CombinationValue { get; set; }

    }
}
