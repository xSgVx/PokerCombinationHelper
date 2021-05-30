using NUnit.Framework;
using System;
using Cards;
using Checker;

namespace Tester
{
    public class Tests
    {
        [Test]
        public void GetDeckTest()
        {
            var testDeck = Card.GetDeck();
            foreach (var e in testDeck)
            {
                Assert.AreNotEqual(e.Suit, null);
                Assert.AreNotEqual(e.Value, null);
            }
        }

        [Test]
        public void GetCardsTest1()
        {
            var randomedCards = Card.GetCards(2);
            foreach(var e in randomedCards)
            {
                Assert.AreNotEqual(e.Suit, 0);
                Assert.AreNotEqual(e.Value, 0);
            }

        }

    }
}