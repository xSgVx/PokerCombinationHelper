using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerCombinationHelper
{
    public class HandParams : IEquatable <HandParams>
    {
        public ComboRanks ComboRank;
        public Card HighCard;
        public List<Card> Combo;

        public bool Equals(HandParams other)
        {
            // If parameter is null return false.
            if (other == null)
            {
                return false;
            }

            return (other.ComboRank == this.ComboRank) && (other.HighCard.Equals(this.HighCard) );
        }

    }
}
