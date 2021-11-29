using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerCombinationHelper
{
    public class HandParams : IEquatable <HandParams>
    {
        public PokerHandRankings HandRank;
        public Card HighCard;

        public bool Equals(HandParams other)
        {
            // If parameter is null return false.
            if (other == null)
            {
                return false;
            }

            return (other.HandRank == this.HandRank) && (other.HighCard.Equals(this.HighCard) );
        }

    }
}
