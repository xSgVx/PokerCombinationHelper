using CardGameBase;
using System.ComponentModel;

namespace Poker.Models;

public class Card : ICard
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
}