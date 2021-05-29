using NUnit.Framework;
using PokerCombinationHelper;
using System;

namespace Tester
{
    public class Tests
    {
        [Test]
        public void GetRandomCardTest()
        {
            var randomedCard = Card.GetRandomCard();
            Assert.AreNotEqual(randomedCard.Suit, 0);
            Assert.AreNotEqual(randomedCard.Value, 0); 
        }

        [Test]
        public void GetCardsTest1()
        {
            var randomedCards = Card.GetCards(2);
            foreach(var e in randomedCards)
            {
                Assert.AreNotEqual(e.Suit, 0 );
                Assert.AreNotEqual(e.Value, 0);
            }

        }

    }
}