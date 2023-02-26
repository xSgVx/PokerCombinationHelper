using CardGameBase;
using CardGameBase.Factories;
using CardGameBase.Models.Comparers;
using Poker.Extensions;
using Poker.Models;

namespace UnitTests
{
    public class PokerProjTests
    {
        PokerDeck pokerdeck;

        [SetUp]
        public void Setup()
        {
            pokerdeck = new();
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

        /*
        [Test]
        public void StraightComboTrueTest()
        {
            var cards = new List<ICard>()
            {
                new Card(CardValue.Six, CardSuit.Hearts),
                new Card(CardValue.Seven, CardSuit.Diamonds),
                new Card(CardValue.Seven, CardSuit.Hearts),
                new Card(CardValue.Queen, CardSuit.Hearts),
                new Card(CardValue.Nine, CardSuit.Hearts),
                new Card(CardValue.Six, CardSuit.Diamonds),
                new Card(CardValue.Eight, CardSuit.Hearts),
                new Card(CardValue.Ten, CardSuit.Hearts)
            };
            
            Assert.That(CombinationHelper.Instance.IsStraight(cards), Is.True);
        }

        [Test]
        public void StraightComboFalseTest()
        {
            var cards = new List<ICard>()
            {
                new Card(CardValue.Six, CardSuit.Hearts),
                new Card(CardValue.Six, CardSuit.Spades),
                new Card(CardValue.Seven, CardSuit.Clubs),
                new Card(CardValue.Ace, CardSuit.Diamonds),
                new Card(CardValue.Nine, CardSuit.Hearts),
                new Card(CardValue.Queen, CardSuit.Spades),
                new Card(CardValue.Jack, CardSuit.Clubs),
                new Card(CardValue.Ten, CardSuit.Diamonds)
            };

            Assert.That(CombinationHelper.Instance.IsStraight(cards), Is.False);
        }

        [Test]
        public void StraightFlushComboTrueTest()
        {
            //6,7,8,9,10 D
            var cards = new List<ICard>()
            {
                new Card(CardValue.Six, CardSuit.Hearts),
                new Card(CardValue.Nine, CardSuit.Diamonds),
                new Card(CardValue.Eight, CardSuit.Clubs),
                new Card(CardValue.Eight, CardSuit.Diamonds),
                new Card(CardValue.Ace, CardSuit.Hearts),
                new Card(CardValue.Seven, CardSuit.Diamonds),
                new Card(CardValue.Ten, CardSuit.Diamonds),
                new Card(CardValue.Six, CardSuit.Diamonds)
            };

            Assert.That(CombinationHelper.Instance.IsStraightFlush(cards), Is.True);
        }

        [Test]
        public void StraightFlushComboFalseTest()
        {
            var cards = new List<ICard>()
            {
                new Card(CardValue.Two, CardSuit.Hearts),
                new Card(CardValue.Five, CardSuit.Diamonds),
                new Card(CardValue.Seven, CardSuit.Hearts),
                new Card(CardValue.Eight, CardSuit.Hearts),
                new Card(CardValue.Ace, CardSuit.Hearts),
                new Card(CardValue.Nine, CardSuit.Hearts),
                new Card(CardValue.Queen, CardSuit.Clubs),
                new Card(CardValue.Ten, CardSuit.Diamonds)
            };

            Assert.That(CombinationHelper.Instance.IsStraightFlush(cards), Is.False);
        }

        [Test]
        public void RoyalFlushComboTrueTest()
        {
            var cards = new List<ICard>()
            {
                new Card(CardValue.Ten, CardSuit.Hearts),
                new Card(CardValue.Queen, CardSuit.Hearts),
                new Card(CardValue.Seven, CardSuit.Clubs),
                new Card(CardValue.King, CardSuit.Diamonds),
                new Card(CardValue.Ace, CardSuit.Diamonds),
                new Card(CardValue.Queen, CardSuit.Diamonds),
                new Card(CardValue.Jack, CardSuit.Diamonds),
                new Card(CardValue.Ten, CardSuit.Diamonds)
            };

            Assert.That(CombinationHelper.Instance.IsRoyalFlush(cards), Is.True);
        }

        [Test]
        public void RoyalFlushComboFalseTest()
        {
            var cards = new List<ICard>()
            {
                new Card(CardValue.Ten, CardSuit.Hearts),
                new Card(CardValue.Queen, CardSuit.Hearts),
                new Card(CardValue.Seven, CardSuit.Clubs),
                new Card(CardValue.King, CardSuit.Diamonds),
                new Card(CardValue.Ace, CardSuit.Diamonds),
                new Card(CardValue.Queen, CardSuit.Diamonds),
                new Card(CardValue.Jack, CardSuit.Hearts),
                new Card(CardValue.Ten, CardSuit.Diamonds)
            };

            Assert.That(CombinationHelper.Instance.IsRoyalFlush(cards), Is.False);
        }

        [Test]
        public void FullHouseTest1()
        {
            var cards = new List<ICard>()
            {
                new Card(CardValue.Ten, CardSuit.Hearts),
                new Card(CardValue.Ten, CardSuit.Clubs),
                new Card(CardValue.Queen, CardSuit.Clubs),
                new Card(CardValue.King, CardSuit.Diamonds),
                new Card(CardValue.Ace, CardSuit.Diamonds),
                new Card(CardValue.Queen, CardSuit.Diamonds),
                new Card(CardValue.Jack, CardSuit.Hearts),
                new Card(CardValue.Ten, CardSuit.Diamonds)
            };

            Assert.That(CombinationHelper.Instance.IsFullHouse(cards), Is.False);
        }

        */
    }
}