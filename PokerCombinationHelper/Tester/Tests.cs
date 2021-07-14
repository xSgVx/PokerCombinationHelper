using NUnit.Framework;
using System;
using Cards;
using Checker;
using System.Collections.Generic;

namespace Tester
{
    public class Tests
    {
        //Тест на неповторяющиеся элементы, уникальных карт  
        //должно получится 52 равное количеству карт в колоде
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

            var equalCardList = CombinationChecker.CheckForEqualCardValueOrSuit(cardList, "Value");

            List<Card> requiredList1 = new List<Card>
            {
                new Card() { Value = (CardValue)7, Suit = (CardSuit)4 },
                new Card() { Value = (CardValue)7, Suit = (CardSuit)1 }
            };

            List<Card> requiredList2 = new List<Card>
            {
                new Card() { Value = (CardValue)6, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)6, Suit = (CardSuit)3 }
            };

            for (int i = 0; i < equalCardList.Count; i++)
            {
                int match = 0;

                if (i == 0)
                {
                    foreach (Card card in requiredList1)
                    {
                        match += equalCardList[i].FindAll(x => x.Value.Equals(card.Value) && x.Suit.Equals(card.Suit)).Count;
                    }
                    Assert.AreEqual(match, 2);
                }

                if (i == 1)
                {
                    foreach (Card card in requiredList2)
                    {
                        match += equalCardList[i].FindAll(x => x.Value.Equals(card.Value) && x.Suit.Equals(card.Suit)).Count;
                    }
                    Assert.AreEqual(match, 2);
                }
            }
        }

        [Test]
        public void CheckForEqualCardValueFullHouse()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)7, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)8, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)10, Suit = (CardSuit)2 },
                new Card() { Value = (CardValue)10, Suit = (CardSuit)3 },
                new Card() { Value = (CardValue)8, Suit = (CardSuit)4 },
                new Card() { Value = (CardValue)10, Suit = (CardSuit)1 }
            };

            var equalCardList = CombinationChecker.CheckForEqualCardValueOrSuit(cardList, "Value");

            List<Card> requiredList1 = new List<Card>
            {
                new Card() { Value = (CardValue)10, Suit = (CardSuit)2 },
                new Card() { Value = (CardValue)10, Suit = (CardSuit)3 },
                new Card() { Value = (CardValue)10, Suit = (CardSuit)1 }
            };

            List<Card> requiredList2 = new List<Card>
            {
                new Card() { Value = (CardValue)8, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)8, Suit = (CardSuit)4 }
            };

            for (int i = 0; i < equalCardList.Count; i++)
            {
                int match = 0;

                if (i == 0)
                {
                    foreach (Card card in requiredList1)
                    {
                        match += equalCardList[i].FindAll(x => x.Value.Equals(card.Value) && x.Suit.Equals(card.Suit)).Count;
                    }
                    Assert.AreEqual(match, 3);
                }

                if (i == 1)
                {
                    foreach (Card card in requiredList2)
                    {
                        match += equalCardList[i].FindAll(x => x.Value.Equals(card.Value) && x.Suit.Equals(card.Suit)).Count;
                    }
                    Assert.AreEqual(match, 2);
                }
            }
        }

        [Test]
        public void CheckForRoyalFlush()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)6, Suit = (CardSuit)3 },
                new Card() { Value = (CardValue)10, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)13, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)12, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)14, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)7, Suit = (CardSuit)2 },
                new Card() { Value = (CardValue)11, Suit = (CardSuit)1 }
            };

            List<List<Card>> equalCardList = CombinationChecker.CheckForEqualCardValueOrSuit(cardList, "Suit");

            Assert.AreEqual(CombinationChecker.CheckForRoyalFlush(equalCardList), true);
        }

        [Test]
        public void CheckForStraightFlush()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)6, Suit = (CardSuit)2 },
                new Card() { Value = (CardValue)10, Suit = (CardSuit)3 },
                new Card() { Value = (CardValue)8, Suit = (CardSuit)2 },
                new Card() { Value = (CardValue)9, Suit = (CardSuit)2 },
                new Card() { Value = (CardValue)5, Suit = (CardSuit)2 },
                new Card() { Value = (CardValue)7, Suit = (CardSuit)2 },
                new Card() { Value = (CardValue)11, Suit = (CardSuit)4 }
            };

            List<List<Card>> equalCardList = CombinationChecker.CheckForEqualCardValueOrSuit(cardList, "Suit");

            Assert.AreEqual(CombinationChecker.CheckForStraightFlush(equalCardList), true);
        }

        [Test]
        public void FindKickerInList()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)6, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)9, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)10, Suit = (CardSuit)2 },
                new Card() { Value = (CardValue)6, Suit = (CardSuit)3 },
                new Card() { Value = (CardValue)7, Suit = (CardSuit)4 },
                new Card() { Value = (CardValue)13, Suit = (CardSuit)3 },
                new Card() { Value = (CardValue)7, Suit = (CardSuit)1 }
            };

            var requiredCard = new Card() { Value = (CardValue)13, Suit = (CardSuit)3 };

            Assert.AreEqual(CombinationChecker.Kicker(cardList).Value, requiredCard.Value);

        }

        [Test]
        public void FindKickerInFullHouse()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)6, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)7, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)12, Suit = (CardSuit)2 },
                new Card() { Value = (CardValue)12, Suit = (CardSuit)3 },
                new Card() { Value = (CardValue)7, Suit = (CardSuit)4 },
                new Card() { Value = (CardValue)12, Suit = (CardSuit)1 }
            };

            var equalCardList = CombinationChecker.CheckForEqualCardValueOrSuit(cardList, "Value");

            var requiredCard = new Card() { Value = (CardValue)12, Suit = (CardSuit)2 };

            Assert.AreEqual(CombinationChecker.Kicker(equalCardList).Value, requiredCard.Value);
        }
    }
}