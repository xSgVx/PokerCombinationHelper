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
        public void DeckCount()
        {
            var deck = Card.GetDeck();

            Assert.AreNotEqual(deck, null);
            Assert.AreEqual(deck.Count, 52);
        }

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
        public void CheckPairTest1()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)2, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)5, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)5, Suit = (CardSuit)2 },
                new Card() { Value = (CardValue)10, Suit = (CardSuit)3 },
                new Card() { Value = (CardValue)14, Suit = (CardSuit)4 }
            };

            cardList.Sort();

            Assert.AreEqual(CombinationChecker.OnePair(cardList), true);
        }

        [Test]
        public void CheckPairTest2()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)14, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)10, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)14, Suit = (CardSuit)2 },
                new Card() { Value = (CardValue)5, Suit = (CardSuit)3 },
                new Card() { Value = (CardValue)10, Suit = (CardSuit)4 }
            };

            cardList.Sort();

            Assert.AreEqual(CombinationChecker.OnePair(cardList), true);
        }

        [Test]
        public void CheckPairTest3()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)7, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)2, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)14, Suit = (CardSuit)2 },
                new Card() { Value = (CardValue)5, Suit = (CardSuit)3 },
                new Card() { Value = (CardValue)10, Suit = (CardSuit)4 }
            };

            cardList.Sort();

            Assert.AreEqual(CombinationChecker.OnePair(cardList), false);
        }

        [Test]
        public void CheckTwoPairsTest1()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)6, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)10, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)10, Suit = (CardSuit)2 },
                new Card() { Value = (CardValue)6, Suit = (CardSuit)3 },
                new Card() { Value = (CardValue)7, Suit = (CardSuit)4 }
            };

            Assert.AreEqual(CombinationChecker.TwoPairs(cardList), true);
        }

        public void CheckTwoPairsTest2()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)7, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)9, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)8, Suit = (CardSuit)2 },
                new Card() { Value = (CardValue)7, Suit = (CardSuit)3 },
                new Card() { Value = (CardValue)11, Suit = (CardSuit)4 }
            };

            Assert.AreEqual(CombinationChecker.TwoPairs(cardList), false);
        }

        public void CheckTwoPairsTest3()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)14, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)8, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)10, Suit = (CardSuit)2 },
                new Card() { Value = (CardValue)6, Suit = (CardSuit)3 },
                new Card() { Value = (CardValue)7, Suit = (CardSuit)4 }
            };

            Assert.AreEqual(CombinationChecker.TwoPairs(cardList), false);
        }

        public void CheckTwoPairsTest4()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)6, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)9, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)10, Suit = (CardSuit)2 },
                new Card() { Value = (CardValue)6, Suit = (CardSuit)3 },
                new Card() { Value = (CardValue)7, Suit = (CardSuit)4 }
            };

            Assert.AreEqual(CombinationChecker.TwoPairs(cardList), false);
        }
    }
}