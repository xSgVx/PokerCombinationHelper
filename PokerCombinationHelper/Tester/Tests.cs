using NUnit.Framework;
using System;
using Cards;
using Checker;
using System.Collections.Generic;

namespace Tester
{
    public class Tests
    {
        [Test]
        public void RepeatedCardsInDeck()
        {
            var deck = Card.GetDeck();
            int matchCount = 0;

            foreach (var card in deck)
            {
                matchCount += deck.FindAll(x => x.Equals(card)).Count;
            }
            Assert.AreEqual(matchCount, 52);
        }

        [Test]
        public void CheckForEqualCardValueTwoPair()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)6, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)9, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)10, Suit = (CardSuit)2 },
                new Card() { Value = (CardValue)6, Suit = (CardSuit)3 },
                new Card() { Value = (CardValue)7, Suit = (CardSuit)4 },
                new Card() { Value = (CardValue)7, Suit = (CardSuit)1 }
            };

            cardList.Sort();

            Assert.IsNotNull(CombinationChecker.CheckForEqualCardValue(cardList));
        }

        [Test]
        public void CheckForEqualCardValueFullHouse()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)10, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)8, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)10, Suit = (CardSuit)2 },
                new Card() { Value = (CardValue)7, Suit = (CardSuit)3 },
                new Card() { Value = (CardValue)10, Suit = (CardSuit)4 },
                new Card() { Value = (CardValue)8, Suit = (CardSuit)1 }
            };

            cardList.Sort();

            Assert.IsNotNull(CombinationChecker.CheckForEqualCardValue(cardList));
        }

    }
}