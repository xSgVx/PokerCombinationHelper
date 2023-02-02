using CardGameBase;
using System.ComponentModel;

namespace Poker.Models;

public enum CardParams
{
    CardValue = 0,
    CardSuit = 1
}

public class Card : ICard, IComparable<Card>
{
    public CardValue Value { get; }
    public CardSuit Suit { get; }

    public Card(CardValue cardValue, CardSuit cardSuit)
    {
        Value = cardValue;
        Suit = cardSuit;
    }

    public int CompareTo(Card? compareCard)
    {
        if (Value < compareCard?.Value)
            return -1;

        if (Value > compareCard?.Value)
            return 1;

        return 0;
    }

    

}