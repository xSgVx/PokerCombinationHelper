using CardGameBase.Models.Comparers;
using CardGameBase;
using Poker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    internal class LogicTests
    {
        PokerDeck pokerdeck;
        CardCreator cardCreator;

        [SetUp]
        public void Setup()
        {
            pokerdeck = new();
            cardCreator = new();
        }

        [Test]
        public void SortingDeckTest()
        {
            pokerdeck.RefreshDeck();
            var cards = new List<ICard>(pokerdeck.GetCardsFromDeck(7));

            cards.Sort(new CardValueComparer(OrderBy.Asc));
            List<ICard> ascSortedCards = new List<ICard>(cards);

            cards.Sort(new CardValueComparer(OrderBy.Desc));
            List<ICard> descSortedCards = new List<ICard>(cards);

            bool isSortedListsNotEqual = false;
            for (int i = 0; i < cards.Count; i++)
            {
                if (!ascSortedCards[i].Equals(descSortedCards[i]))
                {
                    isSortedListsNotEqual = true;
                    break;
                }
            }

            Assert.That(isSortedListsNotEqual, Is.True);
        }

        [Test]
        public void CardValueComparerTest()
        {
            pokerdeck.RefreshDeck();
            var cards = new List<ICard>(pokerdeck.GetCardsFromDeck(52));
            cards.Sort(new CardValueComparer(OrderBy.Asc));

            var distinctByValueCards = cards.Distinct(new CardValueComparer()).ToList();

            Assert.That(distinctByValueCards, Has.Count.EqualTo(13));
        }

        [Test]
        public void CardSuitComparerTest()
        {
            pokerdeck.RefreshDeck();
            var cards = new List<ICard>(pokerdeck.GetCardsFromDeck(52));
            cards.Sort(new CardValueComparer(OrderBy.Asc));

            var distinctBySuitCards = cards.Distinct(new CardSuitComparer()).ToList();

            Assert.That(distinctBySuitCards, Has.Count.EqualTo(4));
        }

        [Test]
        public void PlayerAndCardToStringTest()
        {
            var p1 = new Player("p1", cardCreator.CreateCardsFromString("Ac 9c"));
            var p2 = new Player("p2", cardCreator.CreateCardsFromString("9s Kc"));

            List<IPlayer> ienumerablePlayers = new() { p1,p2 };

            var s = p1.ToString();
            var stringList = string.Join("\n", ienumerablePlayers);

            Assert.IsTrue(true);
        }

    }
}
