using System.Diagnostics.CodeAnalysis;

namespace CardGameBase.Models.Comparers
{
    public class CardSuitComparer : EqualityComparer<ICard>
    {
        public override bool Equals(ICard? x, ICard? y)
        {
            if (x != null && y != null)
            {
                return x.Suit.Equals(y.Suit);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode([DisallowNull] ICard obj)
        {
            return obj.Suit.GetHashCode();
        }
    }
}
