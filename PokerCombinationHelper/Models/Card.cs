﻿using System;
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

        internal Card(CardValue cardValue, CardSuit cardSuit)
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
}