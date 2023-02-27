using CardGameBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameBase.Factories
{
    public abstract class CardGenerator
    {
        public CardGenerator()
        {

        }

        public IEnumerable<ICard> CreateCardsFromString(string s)
        {
            var stringCards = s.Trim().Split(' ');
            ICollection<ICard> cards = new List<ICard>();

            foreach (var stringCard in stringCards)
            {
                cards.Add(ParseCard(stringCard));
            }

            return cards;
        }

        private ICard ParseCard(string s)
        {
            string stringVal;
            string stringSuit;

            if (char.IsDigit(s[0]))    //2-10 or 2-14
            {
                stringVal = new string(s.Where(Char.IsDigit)?.ToArray());
            }
            else    //J-A
            {
                stringVal = new string(s.Where(Char.IsUpper)?.ToArray());
            }

            stringSuit = new string(s.Where(Char.IsLower)?.ToArray());

            return new Card(ParseValue(stringVal),  ParseSuit(stringSuit));
        }

        private CardValue ParseValue(string s)
        {
            switch (s)
            {
                case "2": return CardValue.Two;
                case "3": return CardValue.Three;
                case "4": return CardValue.Four;
                case "5": return CardValue.Five;
                case "6": return CardValue.Six;
                case "7": return CardValue.Seven;
                case "8": return CardValue.Eight;
                case "9": return CardValue.Nine;
                case "10": return CardValue.Ten;
                case "11" or "J": return CardValue.Jack;
                case "12" or "Q": return CardValue.Queen;
                case "13" or "K": return CardValue.King;
                case "14" or "A": return CardValue.Ace;
                default: 
                    throw new Exception("Unknown CardValue");
            }
        }

        private CardSuit ParseSuit(string s)
        {
            switch(s) 
            {
                case "h": return CardSuit.Hearts;
                case "d": return CardSuit.Diamonds;
                case "c": return CardSuit.Clubs;
                case "s": return CardSuit.Spades;
                default:
                    throw new Exception("Unknown CardSuit");
            }

        }
    }
}
