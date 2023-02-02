using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameBase.Models.Comparers
{
    internal class CardValueComparer : EqualityComparer<ICard>
    {
        public override bool Equals(ICard? x, ICard? y)
        {
            if (x != null && y != null)
            {
                return x.Value.Equals(y.Value);
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
