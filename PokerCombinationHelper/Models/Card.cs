using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameBase.Models
{
    internal class Card : ICard
    {
        public CardValue Value { get; }
        public CardSuit Suit { get; }

        public Card(CardValue cardValue, CardSuit cardSuit)
        {
            Value = cardValue;
            Suit = cardSuit;
        }
    }
}
