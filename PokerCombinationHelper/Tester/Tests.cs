using NUnit.Framework;
using System.Collections.Generic;

namespace PokerCombinationHelper
{
    public class Tests
    {
        //Тест на неповторяющиеся элементы, уникальных карт  
        //должно получится 52 равное количеству карт в колоде
        //        [Test]
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
        public void FindHighCardInList()
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

            Assert.AreEqual(requiredCard.Value, CombinationChecker.HighCard(cardList).Value);

        }

        [Test]
        public void RoyalFlushTest()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = CardValue.Nine, Suit = CardSuit.Clubs },
                new Card() { Value = CardValue.Six, Suit = CardSuit.Clubs },
                new Card() { Value = CardValue.Jack, Suit = CardSuit.Clubs },
                new Card() { Value = (CardValue)12, Suit = CardSuit.Clubs },
                new Card() { Value = (CardValue)14, Suit = CardSuit.Clubs },
                new Card() { Value = (CardValue)13, Suit = CardSuit.Clubs },
                new Card() { Value = (CardValue)10, Suit = CardSuit.Clubs }
            };


            var winnerParams = new HandParams
            {
                ComboRank = ComboRanks.RoyalFlush,
                HighCard = new Card
                {
                    Suit = CardSuit.Clubs,
                    Value = CardValue.Ace
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        [Test]
        public void StraightFlushTest()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = CardValue.Nine, Suit = CardSuit.Hearts },
                new Card() { Value = CardValue.Six, Suit = CardSuit.Diamonds },
                new Card() { Value = CardValue.Jack, Suit = CardSuit.Clubs },
                new Card() { Value = (CardValue)8, Suit = CardSuit.Diamonds },
                new Card() { Value = (CardValue)9, Suit = CardSuit.Diamonds },
                new Card() { Value = (CardValue)10, Suit = CardSuit.Diamonds },
                new Card() { Value = (CardValue)7, Suit = CardSuit.Diamonds }
            };


            var winnerParams = new HandParams
            {
                ComboRank = ComboRanks.StraightFlush,
                HighCard = new Card
                {
                    Suit = CardSuit.Diamonds,
                    Value = (CardValue)10
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        [Test]
        public void FourOfAAKindTest()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = CardValue.Ace, Suit = CardSuit.Hearts },
                new Card() { Value = CardValue.Six, Suit = CardSuit.Diamonds },
                new Card() { Value = CardValue.Jack, Suit = CardSuit.Clubs },
                new Card() { Value = CardValue.Ace, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)9, Suit = CardSuit.Spades },
                new Card() { Value = CardValue.Ace, Suit = CardSuit.Clubs },
                new Card() { Value = CardValue.Ace, Suit = CardSuit.Clubs }
            };


            var winnerParams = new HandParams
            {
                ComboRank = ComboRanks.FourOfAKind,
                HighCard = new Card
                {
                    Suit = CardSuit.Hearts,
                    Value = (CardValue)14
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        [Test]
        public void FullHouseTest1()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)2, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)2, Suit = CardSuit.Diamonds },
                new Card() { Value = CardValue.Jack, Suit = CardSuit.Clubs },
                new Card() { Value = (CardValue)10, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)9, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)2, Suit = CardSuit.Clubs },
                new Card() { Value = (CardValue)10, Suit = CardSuit.Clubs }
            };

            var winnerParams = new HandParams
            {
                ComboRank = ComboRanks.FullHouse,
                HighCard = new Card
                {
                    Suit = CardSuit.Hearts,
                    Value = (CardValue)2
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        [Test]
        public void FullHouseTest2()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)10, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)2, Suit = CardSuit.Diamonds },
                new Card() { Value = CardValue.Jack, Suit = CardSuit.Clubs },
                new Card() { Value = (CardValue)10, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)9, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)2, Suit = CardSuit.Clubs },
                new Card() { Value = (CardValue)10, Suit = CardSuit.Clubs }
            };

            var winnerParams = new HandParams
            {
                ComboRank = ComboRanks.FullHouse,
                HighCard = new Card
                {
                    Suit = CardSuit.Hearts,
                    Value = (CardValue)10
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        [Test]
        public void FlushTest()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)6, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)2, Suit = CardSuit.Diamonds },
                new Card() { Value = CardValue.Jack, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)7, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)9, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)2, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)10, Suit = CardSuit.Hearts }
            };

            var winnerParams = new HandParams
            {
                ComboRank = ComboRanks.Flush,
                HighCard = new Card
                {
                    Suit = CardSuit.Hearts,
                    Value = (CardValue)11
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        [Test]
        public void StraightTest()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)3, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)7, Suit = CardSuit.Diamonds },
                new Card() { Value = CardValue.Jack, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)4, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)6, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)5, Suit = CardSuit.Diamonds },
                new Card() { Value = (CardValue)5, Suit = CardSuit.Spades }
            };

            var winnerParams = new HandParams
            {
                ComboRank = ComboRanks.Straight,
                HighCard = new Card
                {
                    Suit = CardSuit.Diamonds,
                    Value = (CardValue)7
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        [Test]
        public void StraightWithAceTest()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)2, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)3, Suit = CardSuit.Diamonds },
                new Card() { Value = (CardValue)5, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)4, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)8, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)14, Suit = CardSuit.Diamonds },
                new Card() { Value = (CardValue)13, Suit = CardSuit.Spades }
            };

            var winnerParams = new HandParams
            {
                ComboRank = ComboRanks.Straight,
                HighCard = new Card
                {
                    Suit = CardSuit.Diamonds,
                    Value = (CardValue)14
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        [Test]
        public void ThreeOfAKindTest()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)3, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)5, Suit = CardSuit.Diamonds },
                new Card() { Value = CardValue.Jack, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)4, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)6, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)5, Suit = CardSuit.Diamonds },
                new Card() { Value = (CardValue)5, Suit = CardSuit.Spades }
            };

            var winnerParams = new HandParams
            {
                ComboRank = ComboRanks.ThreeOfAKind,
                HighCard = new Card
                {
                    Suit = CardSuit.Diamonds,
                    Value = (CardValue)5
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        [Test]
        public void TwoPairTest()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)2, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)8, Suit = CardSuit.Diamonds },
                new Card() { Value = CardValue.Jack, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)4, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)6, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)8, Suit = CardSuit.Diamonds },
                new Card() { Value = (CardValue)2, Suit = CardSuit.Spades }
            };

            var winnerParams = new HandParams
            {
                ComboRank = ComboRanks.TwoPair,
                HighCard = new Card
                {
                    Suit = CardSuit.Diamonds,
                    Value = (CardValue)8
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        [Test]
        public void PairTest()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)10, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)8, Suit = CardSuit.Diamonds },
                new Card() { Value = CardValue.Jack, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)4, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)6, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)2, Suit = CardSuit.Diamonds },
                new Card() { Value = (CardValue)2, Suit = CardSuit.Spades }
            };

            var winnerParams = new HandParams
            {
                ComboRank = ComboRanks.Pair,
                HighCard = new Card
                {
                    Suit = CardSuit.Diamonds,
                    Value = (CardValue)2
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        [Test]
        public void HighCardTest()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)5, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)8, Suit = CardSuit.Diamonds },
                new Card() { Value = CardValue.Jack, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)4, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)7, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)3, Suit = CardSuit.Diamonds },
                new Card() { Value = (CardValue)2, Suit = CardSuit.Spades }
            };

            var winnerParams = new HandParams
            {
                ComboRank = ComboRanks.HighCard,
                HighCard = new Card
                {
                    Suit = CardSuit.Hearts,
                    Value = (CardValue)11
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

    }
}