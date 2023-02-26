using CardGameBase;
using System.ComponentModel;

namespace Poker.Models;

public class Card : ICard /*, IComparable<ICard>, IComparable<Card>*/
{
    public CardValue Value { get; }
    public CardSuit Suit { get; }

    public Card(CardValue cardValue, CardSuit cardSuit)
    {
        Value = cardValue;
        Suit = cardSuit;
    }

    public bool Equals(ICard? other)
    {        
        if (this?.Suit == other?.Suit && this?.Value == other?.Value) 
        {
            return true;
        }

        return false;
    }

    /*
public int CompareTo(Card? compareCard)
{
   if (Value < compareCard?.Value)
       return -1;

   if (Value > compareCard?.Value)
       return 1;

   return 0;
}

public int CompareTo(ICard? other)
{
   if (Value < other?.Value)
       return -1;

   if (Value > other?.Value)
       return 1;

   return 0;
}
*/
}