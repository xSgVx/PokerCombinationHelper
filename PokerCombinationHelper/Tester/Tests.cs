using NUnit.Framework;
using System.Collections.Generic;

namespace PokerCombinationHelper
{
    public class Tests
    {
        [Test]
        public void GetWinnerTest2()
        {
            List<Card> boardCards = new List<Card>
            {
                new Card() { Value = (CardValue)11, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)11, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)7, Suit = CardSuit.Diamonds },
                new Card() { Value = (CardValue)4, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)5, Suit = CardSuit.Hearts }
            };

            var playersList = new List<Player>
            {
                new Player()
                {
                    PlayerCards = new List<Card>()
                    {
                        new Card() { Value = (CardValue)11, Suit = CardSuit.Clubs },
                        new Card() { Value = (CardValue)13, Suit = CardSuit.Hearts }
                    },
                    PlayerName = "Player_0"
                },

                new Player()
                {
                    PlayerCards = new List<Card>()
                    {
                        new Card() { Value = (CardValue)8, Suit = CardSuit.Spades },
                        new Card() { Value = (CardValue)7, Suit = CardSuit.Spades }
                    },
                    PlayerName = "Player_1"
                },

                new Player()
                {
                    PlayerCards = new List<Card>()
                    {
                        new Card() { Value = (CardValue)7, Suit = CardSuit.Spades },
                        new Card() { Value = (CardValue)2, Suit = CardSuit.Diamonds }
                    },
                    PlayerName = "Player_2"
                },

                new Player()
                {
                    PlayerCards = new List<Card>()
                    {
                        new Card() { Value = (CardValue)10, Suit = CardSuit.Spades },
                        new Card() { Value = (CardValue)12, Suit = CardSuit.Diamonds }
                    },
                    PlayerName = "Player_3"
                },

                new Player()
                {
                    PlayerCards = new List<Card>()
                    {
                        new Card() { Value = (CardValue)9, Suit = CardSuit.Hearts },
                        new Card() { Value = (CardValue)3, Suit = CardSuit.Spades }
                    },
                    PlayerName = "Player_4"
                },

                new Player()
                {
                    PlayerCards = new List<Card>()
                    {
                        new Card() { Value = (CardValue)3, Suit = CardSuit.Diamonds },
                        new Card() { Value = (CardValue)14, Suit = CardSuit.Hearts }
                    },
                    PlayerName = "Player_5"
                },

            };

            var winnerParams = new HandParams
            {
                ComboRank = ComboRanks.ThreeOfAKind,
                HighCard = new Card
                {
                    Suit = CardSuit.Clubs,
                    Value = CardValue.Jack
                },
                Combo = new List<Card>
                {
                    new Card() { Value = (CardValue)11, Suit = CardSuit.Spades },
                    new Card() { Value = (CardValue)11, Suit = CardSuit.Hearts },
                    new Card() { Value = (CardValue)11, Suit = CardSuit.Clubs },
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetWinner(playersList, boardCards).HandParams);
        }


        [Test]
        public void GetWinnerTest1()
        {
            List<Card> boardCards = new List<Card>
            {
                new Card() { Value = (CardValue)2, Suit = CardSuit.Clubs },
                new Card() { Value = (CardValue)3, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)9, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)12, Suit = CardSuit.Diamonds },
                new Card() { Value = (CardValue)13, Suit = CardSuit.Spades }
            };

            var playersList = new List<Player>
            {
                new Player()
                {
                    PlayerCards = new List<Card>()
                    {
                        new Card() { Value = (CardValue)11, Suit = CardSuit.Clubs },
                        new Card() { Value = (CardValue)5, Suit = CardSuit.Spades }
                    },
                    PlayerName = "Player_0"
                },

                new Player()
                {
                    PlayerCards = new List<Card>()
                    {
                        new Card() { Value = (CardValue)11, Suit = CardSuit.Hearts },
                        new Card() { Value = (CardValue)4, Suit = CardSuit.Spades }
                    },
                    PlayerName = "Player_1"
                },

                new Player()
                {
                    PlayerCards = new List<Card>()
                    {
                        new Card() { Value = (CardValue)14, Suit = CardSuit.Hearts },
                        new Card() { Value = (CardValue)5, Suit = CardSuit.Clubs }
                    },
                    PlayerName = "Player_2"
                },

                new Player()
                {
                    PlayerCards = new List<Card>()
                    {
                        new Card() { Value = (CardValue)9, Suit = CardSuit.Diamonds },
                        new Card() { Value = (CardValue)2, Suit = CardSuit.Diamonds }
                    },
                    PlayerName = "Player_3"
                }
            };

            var winnerParams = new HandParams
            {
                ComboRank = ComboRanks.TwoPair,
                HighCard = new Card
                {
                    Suit = CardSuit.Diamonds,
                    Value = CardValue.Nine
                },
                Combo = new List<Card>
                {
                    new Card() { Value = (CardValue)2, Suit = CardSuit.Diamonds },
                    new Card() { Value = (CardValue)2, Suit = CardSuit.Clubs },
                    new Card() { Value = (CardValue)9, Suit = CardSuit.Diamonds },
                    new Card() { Value = (CardValue)9, Suit = CardSuit.Hearts }
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetWinner(playersList,boardCards).HandParams);
        }

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

        //[Test]
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
        public void CustomTest1()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)2, Suit = CardSuit.Clubs },
                new Card() { Value = (CardValue)3, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)9, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)12, Suit = CardSuit.Diamonds },
                new Card() { Value = (CardValue)13, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)9, Suit = CardSuit.Diamonds },
                new Card() { Value = (CardValue)2, Suit = CardSuit.Diamonds }
            };


            var winnerParams = new HandParams
            {
                ComboRank = ComboRanks.TwoPair,
                HighCard = new Card
                {
                    Suit = CardSuit.Hearts,
                    Value = (CardValue)9
                },
                Combo = new List<Card>
                {
                    new Card() { Value = (CardValue)2, Suit = CardSuit.Clubs },
                    new Card() { Value = (CardValue)2, Suit = CardSuit.Diamonds },
                    new Card() { Value = (CardValue)9, Suit = CardSuit.Hearts },
                    new Card() { Value = (CardValue)9, Suit = CardSuit.Diamonds }
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        //[Test]
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
                },
                Combo = new List<Card>
                {
                    new Card() { Value = CardValue.Jack, Suit = CardSuit.Clubs },
                    new Card() { Value = (CardValue)12, Suit = CardSuit.Clubs },
                    new Card() { Value = (CardValue)14, Suit = CardSuit.Clubs },
                    new Card() { Value = (CardValue)13, Suit = CardSuit.Clubs },
                    new Card() { Value = (CardValue)10, Suit = CardSuit.Clubs }
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        //[Test]
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
                },
                Combo = new List<Card>
                {
                    new Card() { Value = CardValue.Six, Suit = CardSuit.Diamonds },
                    new Card() { Value = (CardValue)8, Suit = CardSuit.Diamonds },
                    new Card() { Value = (CardValue)9, Suit = CardSuit.Diamonds },
                    new Card() { Value = (CardValue)10, Suit = CardSuit.Diamonds },
                    new Card() { Value = (CardValue)7, Suit = CardSuit.Diamonds }
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        //[Test]
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
                new Card() { Value = CardValue.Ace, Suit = CardSuit.Diamonds }
            };


            var winnerParams = new HandParams
            {
                ComboRank = ComboRanks.FourOfAKind,
                HighCard = new Card
                {
                    Suit = CardSuit.Hearts,
                    Value = (CardValue)14
                },
                Combo = new List<Card>
                {
                    new Card() { Value = CardValue.Ace, Suit = CardSuit.Hearts },
                    new Card() { Value = CardValue.Ace, Suit = CardSuit.Spades },
                    new Card() { Value = CardValue.Ace, Suit = CardSuit.Clubs },
                    new Card() { Value = CardValue.Ace, Suit = CardSuit.Clubs }
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        //[Test]
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
                },
                Combo = new List<Card>
                {
                    new Card() { Value = (CardValue)2, Suit = CardSuit.Hearts },
                    new Card() { Value = (CardValue)2, Suit = CardSuit.Diamonds },
                    new Card() { Value = (CardValue)2, Suit = CardSuit.Clubs },
                    new Card() { Value = (CardValue)10, Suit = CardSuit.Spades },
                    new Card() { Value = (CardValue)10, Suit = CardSuit.Clubs }
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        //[Test]
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
                },
                Combo = new List<Card>
                 {
                     new Card() { Value = (CardValue)10, Suit = CardSuit.Hearts },
                     new Card() { Value = (CardValue)10, Suit = CardSuit.Spades },
                     new Card() { Value = (CardValue)10, Suit = CardSuit.Clubs },
                     new Card() { Value = (CardValue)2, Suit = CardSuit.Diamonds },
                     new Card() { Value = (CardValue)2, Suit = CardSuit.Clubs }
                 }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        //[Test]
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
                },
                Combo = new List<Card>
                {
                    new Card() { Value = (CardValue)2, Suit = CardSuit.Diamonds },
                    new Card() { Value = (CardValue)2, Suit = CardSuit.Hearts },
                    new Card() { Value = (CardValue)6, Suit = CardSuit.Hearts },
                    new Card() { Value = (CardValue)7, Suit = CardSuit.Hearts },
                    new Card() { Value = (CardValue)9, Suit = CardSuit.Spades }
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        //[Test]
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
                },
                Combo = new List<Card>
                {
                    new Card() { Value = (CardValue)3, Suit = CardSuit.Spades },
                    new Card() { Value = (CardValue)4, Suit = CardSuit.Hearts },
                    new Card() { Value = (CardValue)5, Suit = CardSuit.Spades },
                    new Card() { Value = (CardValue)6, Suit = CardSuit.Spades },
                    new Card() { Value = (CardValue)7, Suit = CardSuit.Diamonds }
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        //[Test]
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
                },
                Combo = new List<Card>()
                {
                    new Card() { Value = (CardValue)2, Suit = CardSuit.Spades },
                    new Card() { Value = (CardValue)3, Suit = CardSuit.Diamonds },
                    new Card() { Value = (CardValue)5, Suit = CardSuit.Hearts },
                    new Card() { Value = (CardValue)4, Suit = CardSuit.Hearts },
                    new Card() { Value = (CardValue)14, Suit = CardSuit.Diamonds }
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        //[Test]
        public void ThreeOfAKindTest()
        {
            List<Card> cardList = new List<Card>
            {
                new Card() { Value = (CardValue)3, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)5, Suit = CardSuit.Diamonds },
                new Card() { Value = CardValue.Jack, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)4, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)6, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)5, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)5, Suit = CardSuit.Spades }
            };

            var winnerParams = new HandParams
            {
                ComboRank = ComboRanks.ThreeOfAKind,
                HighCard = new Card
                {
                    Suit = CardSuit.Diamonds,
                    Value = (CardValue)5
                },
                Combo = new List<Card>
                {
                    new Card() { Value = (CardValue)5, Suit = CardSuit.Diamonds },
                    new Card() { Value = (CardValue)5, Suit = CardSuit.Hearts },
                    new Card() { Value = (CardValue)5, Suit = CardSuit.Spades }
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        //[Test]
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
                },
                Combo = new List<Card>
                {
                    new Card() { Value = (CardValue)2, Suit = CardSuit.Spades },
                    new Card() { Value = (CardValue)8, Suit = CardSuit.Diamonds },
                    new Card() { Value = (CardValue)8, Suit = CardSuit.Diamonds },
                    new Card() { Value = (CardValue)2, Suit = CardSuit.Spades }
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        //[Test]
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
                },
                Combo = new List<Card>
                {
                    new Card() { Value = (CardValue)2, Suit = CardSuit.Diamonds },
                    new Card() { Value = (CardValue)2, Suit = CardSuit.Spades }
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

        //[Test]
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
                },
                Combo = new List<Card>
                {
                    new Card() { Value = CardValue.Jack, Suit = CardSuit.Hearts }
                }
            };

            Assert.AreEqual(winnerParams, CombinationChecker.GetPlayerHandParams(cardList));
        }

    }
}