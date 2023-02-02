using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameBase.Models.Comparers
{
    internal class CardSuitValueComparer : EqualityComparer<ICard>
    {
        public override bool Equals(ICard? x, ICard? y)
        {
            if (x != null && y != null)
            {
                return x.Value.Equals(y.Value) && x.Suit.Equals(y.Suit);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode([DisallowNull] ICard obj)
        {
            return obj.GetHashCode();
        }
    }
}
