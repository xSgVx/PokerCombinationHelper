using System.Diagnostics.CodeAnalysis;

namespace CardGameBase.Models.Comparers
{
    public enum OrderBy
    {
        Asc,
        Desc
    }

    public class CardValueComparer : EqualityComparer<ICard>, IComparer<ICard>
    {
        private OrderBy _orderBy;

        public CardValueComparer(OrderBy orderBy)
        {
            this._orderBy = orderBy;
        }

        public CardValueComparer()
        {

        }

        public int Compare(ICard? x, ICard? y)
        {
            switch (_orderBy)
            {
                case OrderBy.Asc:
                    {
                        if (x?.Value < y?.Value)
                            return -1;

                        if (x?.Value > y?.Value)
                            return 1;
                    }

                    break;
                case OrderBy.Desc:
                    {
                        if (x?.Value > y?.Value)
                            return -1;

                        if (x?.Value < y?.Value)
                            return 1;
                    }

                    break;
                default:
                    break;
            }

            return 0;
        }

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
            return obj.Value.GetHashCode();
        }
    }
}
