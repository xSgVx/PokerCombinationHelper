using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PokerCombinationHelper
{
    public enum ComboRanks
    {
        [Description("Флеш рояль")] RoyalFlush = 10,
        [Description("Стрит флеш")] StraightFlush = 9,
        [Description("Карэ")] FourOfAKind = 8,
        [Description("Фулл хаус")] FullHouse = 7,
        [Description("Флеш")] Flush = 6,
        [Description("Стрит")] Straight = 5,
        [Description("Сет (Тройка)")] ThreeOfAKind = 4,
        [Description("Две пары")] TwoPair = 3,
        [Description("Пара")] Pair = 2,
        [Description("Старшая карта")] HighCard = 1,
        [Description("Ничья")] Draw = 0,
    }

    public enum CardParams
    {
        [Description("Масть")] Suit = 2,
        [Description("Величина")] Value = 1,
    }

    public class CombinationChecker
    {
        public static Player GetWinner(List<Player> playersList, List<Card> boardCards)
        {
            if (playersList == null) return null;

            int match = 0;
            Player winner = null;
            HandParams boardParams = CombinationChecker.GetPlayerHandParams(boardCards);
            List<HandParams> allCardsParams = new List<HandParams>();

            //Блок для случая, в котором комбо находится на столе, а в руках у игроков пусто
            //В таком случае победитель определяется по старшей карте в руке
            for (int i = 0; i < playersList.Count; i++)
            {
                var allCards = playersList[i].PlayerCards.Concat(boardCards).ToList();

                allCardsParams.Add(CombinationChecker.GetPlayerHandParams(allCards));

                if ((boardParams.ComboRank == allCardsParams[i].ComboRank) && (boardParams.HighCard.Value == allCardsParams[i].HighCard.Value))
                {
                    match++;
                }

                if (match == playersList.Count - 1)
                {
                    playersList.ForEach(x => x.HandParams.ComboRank = boardParams.ComboRank);
                    playersList.ForEach(x => x.HandParams.HighCard = CombinationChecker.HighCard(x.PlayerCards));
                }
            }
            if (match != playersList.Count)
            {
                for (int i = 0; i < playersList.Count; i++)
                {
                    playersList[i].HandParams = allCardsParams[i];
                }
            }

            playersList = playersList.OrderByDescending(x => x.HandParams.ComboRank).ToList();
            winner = playersList.First();
            playersList.RemoveAt(0);
            
            if  (playersList.Any(player => player.HandParams.ComboRank == winner.HandParams.ComboRank))
            {
                if (playersList.Any(player2 => player2.HandParams.HighCard != winner.HandParams.HighCard))
                {
                    var list = new List<Player>();
                    list = playersList.FindAll(x => x.HandParams.ComboRank == winner.HandParams.ComboRank && x.HandParams.HighCard != winner.HandParams.HighCard);
                    winner = list.OrderByDescending(x => x.HandParams.HighCard).First();
                }
                else  if (playersList.Any(player2 => player2.HandParams.HighCard == winner.HandParams.HighCard))
                {
                    winner.HandParams.ComboRank = ComboRanks.Draw;
                }
            }
            return winner;
        }

        public static HandParams GetPlayerHandParams(List<Card> inputCardList)
        {
            inputCardList.Sort();

            List<List<Card>> equalCardValueLists = MakeEqualCardValueOrSuitLists(inputCardList, CardParams.Value);
            List<List<Card>> equalCardSuitLists = MakeEqualCardValueOrSuitLists(inputCardList, CardParams.Suit);

            var royalFlush = CheckForRoyalFlush(equalCardSuitLists);
            if (royalFlush != null)
            {
                return new HandParams
                {
                    ComboRank = ComboRanks.RoyalFlush,
                    HighCard = HighCard(inputCardList),
                    Combo = royalFlush
                };
            }

            var straightFlush = StraightFlushlist(equalCardSuitLists);
            if (straightFlush != null)
            {
                return new HandParams
                {
                    ComboRank = ComboRanks.StraightFlush,
                    HighCard = HighCard(straightFlush),
                    Combo = straightFlush
                };
            }

            var fourOfAKind = FourOfAKindList(equalCardValueLists);
            if (fourOfAKind != null)
            {
                return new HandParams
                {
                    ComboRank = ComboRanks.FourOfAKind,
                    HighCard = HighCard(fourOfAKind),
                    Combo = fourOfAKind
                };
            }

            var threeOfAKind = EqualPairsList(equalCardValueLists, 1, 3);
            var pair = EqualPairsList(equalCardValueLists, 1, 2);
            var fullHouse = (threeOfAKind != null) && (pair != null);
            if (fullHouse)
            {
                return new HandParams
                {
                    ComboRank = ComboRanks.FullHouse,
                    HighCard = HighCard(threeOfAKind),
                    Combo = threeOfAKind.Concat(pair).ToList()
                };
            }

            var flush = FlushList(equalCardSuitLists);
            if (flush != null)
            {
                return new HandParams
                {
                    ComboRank = ComboRanks.Flush,
                    HighCard = HighCard(flush),
                    Combo = flush
                };
            }

            var straight = StraightList(inputCardList);
            if (straight != null)
            {
                return new HandParams
                {
                    ComboRank = ComboRanks.Straight,
                    HighCard = HighCard(straight),
                    Combo = straight
                };
            }

            if (threeOfAKind != null)
            {
                return new HandParams
                {
                    ComboRank = ComboRanks.ThreeOfAKind,
                    HighCard = HighCard(threeOfAKind),
                    Combo = threeOfAKind
                };
            }

            var twoPairs = EqualPairsList(equalCardValueLists, 2, 2);
            if (twoPairs != null)
            {
                return new HandParams
                {
                    ComboRank = ComboRanks.TwoPair,
                    HighCard = HighCard(twoPairs),
                    Combo = twoPairs
                };
            }

            if (pair != null)
            {
                return new HandParams
                {
                    ComboRank = ComboRanks.Pair,
                    HighCard = HighCard(pair),
                    Combo = pair
                };
            }

            var highCard = HighCard(inputCardList);
            return new HandParams
            {
                ComboRank = ComboRanks.HighCard,
                HighCard = HighCard(inputCardList),
                Combo = new List<Card> { highCard }
            };

        }

        public static List<List<Card>> MakeEqualCardValueOrSuitLists(List<Card> inputCardList, CardParams cardParam)
        {
            List<List<Card>> equalCardsLists = new List<List<Card>>();

            for (int i = 0; i < inputCardList.Count;)
            {
                List<Card> equalCardsArray = new List<Card>();

                //Берем один элемент списка (карту) и ищем повторение
                //во всем списке по значению величины или масти карты
                switch (cardParam)
                {
                    case CardParams.Value:
                        {
                            equalCardsArray = inputCardList.FindAll(card => card.Value.Equals(inputCardList[i].Value));
                            break;
                        }
                    case CardParams.Suit:
                        {
                            equalCardsArray = inputCardList.FindAll(card => card.Suit.Equals(inputCardList[i].Suit));
                            break;
                        }
                }

                //Если повторяющихся элементов нет, то equalCardsArray.Count равняется 1,
                //т.к в списке элемент нашел только себя
                if (equalCardsArray.Count > 1)
                {
                    equalCardsLists.Add(equalCardsArray);
                }

                //Смещаем индекс массива на количество найденных элементов
                i += equalCardsArray.Count;
            }

            return equalCardsLists.Any() ? equalCardsLists : null;
        }

        private static List<Card> CheckForRoyalFlush(List<List<Card>> inputCardLists)
        {
            if (inputCardLists == null) return null;

            //На вход данной функции должен поступать выход функции 
            //CheckForEqualCardValueOrSuit с типом Suit на входе, поэтому
            //для создания straightFlushList не важно какой будет параметр CardSuit
            List<Card> straightFlushList = new List<Card>()
            {
                new Card() { Value = (CardValue)14, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)13, Suit = (CardSuit)2 },
                new Card() { Value = (CardValue)12, Suit = (CardSuit)3 },
                new Card() { Value = (CardValue)11, Suit = (CardSuit)4 },
                new Card() { Value = (CardValue)10, Suit = (CardSuit)1 },
            };

            var straightFlushCombo = new List<Card>();

            for (int i = 0; i < inputCardLists.Count; i++)
            {
                //Стрит флеш всегда содержит в себе 5 карт
                if (inputCardLists[i].Count < 5) continue;

                straightFlushCombo = inputCardLists[i].Where(inputCardListCard =>
                                                             straightFlushList.Any(straightFlushListCard =>
                                                             straightFlushListCard.Value == inputCardListCard.Value)).Distinct(new CardValueComparer()).ToList();
            }

            if (straightFlushCombo.Count == 5)
                return straightFlushCombo;

            return null;
        }

        public static List<Card> IncreasingSequenceCardsList(List<Card> inputCardList)
        {
            inputCardList = inputCardList.Distinct(new CardValueComparer ()).ToList();
            
            foreach (Card card in inputCardList)
            {
                var matchList = new List<Card>();

                matchList.Add(card);

                for (int i = 1; i <= 4; i++)
                {
                    if (!inputCardList.Any(card2 => card2.Value == card.Value + i))
                        break;
                    matchList.Add(inputCardList.First(card2 => card2.Value == card.Value + i));
                }

                if (matchList.Count == 5)
                {
                    return matchList;
                }
            }
            return null;
        }

        public static List<Card> StraightFlushlist(List<List<Card>> inputCardLists)
        {
            if (inputCardLists == null) return null;

            foreach (var equalCardsList in inputCardLists)
            {
                var increasingCardsList = IncreasingSequenceCardsList(equalCardsList);

                if ((increasingCardsList != null) && (increasingCardsList.Count == 5))
                {
                    return increasingCardsList;
                }
            }
            return null;
        }

        public static List<Card> EqualPairsList(List<List<Card>> inputCardLists, int neededMatchCount, int neededCardCount)
        {
            if (inputCardLists == null) return null;

            int match = 0;
            var pairsLists = new List<Card>();

            foreach (List<Card> equalCardsList in inputCardLists)
            {
                if (equalCardsList.Count == neededCardCount)
                {
                    match++;
                    pairsLists = pairsLists.Concat(equalCardsList).ToList();
                }
            }
            return (match == neededMatchCount) ? pairsLists : null;
        }

        public static List<Card> FourOfAKindList(List<List<Card>> inputCardLists)
        {
            if (inputCardLists == null) return null;

            foreach (List<Card> equalCardsList in inputCardLists)
            {
                if (equalCardsList.Count == 4)
                {
                    return equalCardsList;
                }
            }
            return null;
        }

        public static List<Card> FlushList(List<List<Card>> inputCardLists)
        {
            if (inputCardLists == null) return null;

            foreach (var equalCardsList in inputCardLists)
            {
                if (equalCardsList.Count >= 5)
                {
                    return equalCardsList.Skip(equalCardsList.Count - 5).ToList();
                }
            }
            return null;
        }

        public static List<Card> StraightList(List<Card> inputCardList)
        {
            List<Card> aceCardList = new List<Card>
            {
                new Card() { Value = (CardValue)2, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)3, Suit = CardSuit.Diamonds },
                new Card() { Value = (CardValue)4, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)5, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)14, Suit = CardSuit.Diamonds },
            };

            var query = inputCardList.Where(inputCardListCard =>
                                             aceCardList.Any(aceCardListCard =>
                                             aceCardListCard.Value == inputCardListCard.Value)).Distinct(new CardValueComparer()).ToList();
            if (query.Count == 5)
            {
                return query;
            }

            var increasingCardsList = IncreasingSequenceCardsList(inputCardList);

            if ((increasingCardsList != null))
            {
                return increasingCardsList;
            }

            return null;
        }

        public static Card HighCard(List<Card> inputCardList)
        {
            return inputCardList.Max();
        }

    }
}
