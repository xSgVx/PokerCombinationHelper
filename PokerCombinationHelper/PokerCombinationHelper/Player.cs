using System.Collections.Generic;

namespace PokerCombinationHelper
{
    public class Player
    {
        public string PlayerName { get; set; }
        public List<Card> PlayerCards { get; set; }
        public HandParams HandParams { get; set; }

    }
}
