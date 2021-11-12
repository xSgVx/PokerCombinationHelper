using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PokerCombinationHelper
{
    public enum PokerHandRankings
    {
        [Description("Флеш рояль")] RoyalFlush = 10,
        [Description("Стрит флеш")] StarightFlush = 9,
        [Description("Карэ")] FourOfAKind = 8,
        [Description("Фулл хаус")] FullHouse = 7,
        [Description("Флеш")] Flush = 6,
        [Description("Стрит")] Straight = 5,
        [Description("Сет")] ThreeOfAKind = 4,
        [Description("Две пары")] TwoPair = 3,
        [Description("Пара")] Pair = 2,
        [Description("Старшая карта")] HighCard = 1,
    }

    public class CombinationChecker
    {
        public static WinnerParams GetPlayerHandRank(List<Card> inputCardList)
        {
            inputCardList.Sort();

            List<List<Card>> equalCardValueLists = MakeEqualCardValueOrSuitLists(inputCardList, "Value");
            List<List<Card>> equalCardSuitLists = MakeEqualCardValueOrSuitLists(inputCardList, "Suit");

            var isRoyalFlush = CheckForRoyalFlush(equalCardSuitLists);
            if (isRoyalFlush)
            {
                return new WinnerParams
                {
                    HandRank = (PokerHandRankings)10,
                    HighCard = HighCard(inputCardList)
                };
            }

            var isStraightFlush = (StraightFlushHighCard(equalCardSuitLists) != null);
            if (isStraightFlush)
            {

                return new WinnerParams
                {
                    HandRank = (PokerHandRankings)9,
                    HighCard = StraightFlushHighCard(equalCardSuitLists)
                };
            }

            var isFourOfAKind = FourOfAKindHighCard(equalCardValueLists) != null;
            if (isFourOfAKind)
            {
                return new WinnerParams
                {
                    HandRank = (PokerHandRankings)8,
                    HighCard = FourOfAKindHighCard(equalCardValueLists)
                };
            }
            var isFullHouse = EqualPairsHighCard(equalCardValueLists, 1, 3) != null && EqualPairsHighCard(equalCardValueLists, 1, 2) != null;
            if (isFullHouse)
            {
                return new WinnerParams
                {
                    HandRank = (PokerHandRankings)7,
                    HighCard = EqualPairsHighCard(equalCardValueLists, 1, 3)
                };
            }

            var isFlush = HighCard(equalCardSuitLists) != null;
            if (isFlush)
            {
                return new WinnerParams
                {
                    HandRank = (PokerHandRankings)6,
                    HighCard = HighCard(equalCardSuitLists)
                };
            }

            var isStraight = StraightHighCard(inputCardList, 5) != null;
            if (isStraight)
            {
                return new WinnerParams
                {
                    HandRank = (PokerHandRankings)5,
                    HighCard = StraightHighCard(inputCardList, 5)
                };
            }

            var isThreeOfAKind = EqualPairsHighCard(equalCardValueLists, 1, 3) != null;
            if (isThreeOfAKind)
            {
                return new WinnerParams
                {
                    HandRank = (PokerHandRankings)4,
                    HighCard = EqualPairsHighCard(equalCardValueLists, 1, 3)
                };
            }

            var isTwoPairs = EqualPairsHighCard(equalCardValueLists, 2, 2) != null;
            if (isTwoPairs)
            {
                return new WinnerParams
                {
                    HandRank = (PokerHandRankings)3,
                    HighCard = EqualPairsHighCard(equalCardValueLists, 2, 2)
                };
            }

            var isPair = EqualPairsHighCard(equalCardValueLists, 1, 2) != null;
            if (isPair)
            {
                return new WinnerParams
                {
                    HandRank = (PokerHandRankings)2,
                    HighCard = EqualPairsHighCard(equalCardValueLists, 1, 2)
                };
            }

            var isHighCard = HighCard(inputCardList);
            return new WinnerParams
            {
                HandRank = (PokerHandRankings)1,
                HighCard = HighCard(inputCardList)
            };

        }

        public static List<List<Card>> MakeEqualCardValueOrSuitLists(List<Card> inputCardList, string type)
        {
            inputCardList.Sort();

            List<List<Card>> equalCardsLists = new List<List<Card>>();

            for (int i = 0; i < inputCardList.Count;)
            {
                List<Card> equalCardsArray = new List<Card>();

                //Берем один элемент списка (карту) и ищем повторение
                //во всем списке по значению величины или масти карты
                if (type == "Value")
                {
                    equalCardsArray = inputCardList.FindAll(x => x.Value.Equals(inputCardList[i].Value));
                }
                else if (type == "Suit")
                {
                    equalCardsArray = inputCardList.FindAll(x => x.Suit.Equals(inputCardList[i].Suit));
                }
                else throw new Exception("Неверный тип свойства");

                //Если повторяющихся элементов нет, то equalCardsArray.Count равняется 1,
                //т.к в списке элемент нашел только себя
                if (equalCardsArray.Count > 1)
                {
                    equalCardsLists.Add(equalCardsArray);
                }

                //Смещаем индекс массива на количество найденных элементов
                i += equalCardsArray.Count;
            }

            if (equalCardsLists.Any())
            {
                return equalCardsLists;
            }
            else
            {
                return null;
            }
        }

        private static bool CheckForRoyalFlush(List<List<Card>> inputCardLists)
        {
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

            int match = 0;

            for (int i = 0; i < inputCardLists.Count; i++)
            {
                //Стрит флеш всегда содержит в себе 5 карт
                if (inputCardLists[i].Count < 5)
                {
                    return false;
                }

                foreach (Card card in straightFlushList)
                {
                    match += inputCardLists[i].FindAll(x => x.Value.Equals(card.Value)).Count;
                }
            }

            if (match == 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static int IncreasingSequenceCardsCount(List<Card> inputCardList)
        {
            inputCardList.Sort();
            int match = 1;

            for (int i = 0; i < inputCardList.Count - 1; i++)
            {
                if ((int)inputCardList[i].Value == ((int)inputCardList[i + 1].Value + 1))
                {
                    match++;
                }
            }

            return match;
        }

        private static int EqualSequenceCardsCount(List<Card> inputCardList)
        {
            inputCardList.Sort();
            int match = 1;

            for (int i = 0; i < inputCardList.Count - 1; i++)
            {
                if ((int)inputCardList[i].Value == ((int)inputCardList[i + 1].Value))
                {
                    match++;
                }
            }

            return match;
        }

        private static Card StraightFlushHighCard(List<List<Card>> inputCardsLists)
        {
            foreach (var equalCardsList in inputCardsLists)
            {
                if (IncreasingSequenceCardsCount(equalCardsList) == 5)
                {
                    return HighCard(equalCardsList);
                }
            }
            return null;
        }

        private static Card EqualPairsHighCard(List<List<Card>> inputCardLists, int neededMatchCount, int neededCardCount)
        {
            Card highCard = null;
            int match = 0;

            foreach (List<Card> equalCardsList in inputCardLists)
            {
                if (EqualSequenceCardsCount(equalCardsList) == neededCardCount)
                {
                    highCard = HighCard(equalCardsList);
                    match++;
                }
            }

            if (match == neededMatchCount)
            {
                return highCard;
            }
            else
            {
                return null;
            }
        }

        private static Card FourOfAKindHighCard(List<List<Card>> inputCardLists)
        {
            foreach (List<Card> equalCardsList in inputCardLists)
            {
                if (EqualSequenceCardsCount(equalCardsList) == 4)
                {
                    return HighCard(equalCardsList);
                }
            }

            return null;
        }

        private static Card StraightHighCard(List<Card> inputCardLists, int n)
        {
            inputCardLists.Sort();

            if (IncreasingSequenceCardsCount(inputCardLists) == n)
            {
                for (int i = 0; i < inputCardLists.Count; i++)
                {
                    if ((int)inputCardLists[i].Value == (int)(inputCardLists[i + 1].Value + 1))
                    {
                        return inputCardLists[i];
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return null;
        }

        private static Card HighCard(List<List<Card>> inputCardLists)
        {
            foreach (List<Card> equalCardsList in inputCardLists)
            {
                if (equalCardsList.Count == 5)
                {
                    return HighCard(equalCardsList);
                }
            }

            return null;
        }

        public static Card HighCard(List<Card> inputCardList)
        {
            inputCardList.Sort();

            return inputCardList[0];
        }

    }
}
