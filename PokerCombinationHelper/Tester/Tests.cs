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

            Assert.IsNotNull(CombinationChecker.CheckForEqualCardValueOrSuit(cardList, "Value"));
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

            Assert.IsNotNull(CombinationChecker.CheckForEqualCardValueOrSuit(cardList, "Value"));

            //Непонятно в чем ошибка
            //List<Card> desiredList1 = new List<Card>
            //{
            //    new Card() { Value = (CardValue)10, Suit = (CardSuit)2 },
            //    new Card() { Value = (CardValue)10, Suit = (CardSuit)3 },
            //    new Card() { Value = (CardValue)10, Suit = (CardSuit)1 }
            //};

            //List<Card> desiredList2 = new List<Card>
            //{
            //    new Card() { Value = (CardValue)8, Suit = (CardSuit)1 },
            //    new Card() { Value = (CardValue)8, Suit = (CardSuit)4 }
            //};

            //Dictionary<List<Card>, int> desiredOutput = new Dictionary<List<Card>, int>
            //{
            //    [desiredList1] = 3,
            //    [desiredList2] = 2
            //};

            //Assert.AreEqual(CombinationChecker.CheckForEqualCardValue(cardList, "Value"), desiredOutput);
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

            Dictionary<List<Card>, int> desiredOutput = CombinationChecker.CheckForEqualCardValueOrSuit(cardList, "Suit");

            Assert.AreEqual(CombinationChecker.CheckForRoyalFlush(desiredOutput), true);
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

            Dictionary<List<Card>, int> desiredOutput = CombinationChecker.CheckForEqualCardValueOrSuit(cardList, "Suit");

            Assert.AreEqual(CombinationChecker.CheckForStraightFlush(desiredOutput), true);
        }

    }
}