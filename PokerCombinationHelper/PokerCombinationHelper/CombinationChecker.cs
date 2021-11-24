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
        [Description("Стрит флеш")] StraightFlush = 9,
        [Description("Карэ")] FourOfAKind = 8,
        [Description("Фулл хаус")] FullHouse = 7,
        [Description("Флеш")] Flush = 6,
        [Description("Стрит")] Straight = 5,
        [Description("Сет")] ThreeOfAKind = 4,
        [Description("Две пары")] TwoPair = 3,
        [Description("Пара")] Pair = 2,
        [Description("Старшая карта")] HighCard = 1,
    }

    public enum CardParams
    {
        [Description("Масть")] Suit = 2,
        [Description("Величина")] Value = 1,
    }

    public class CombinationChecker
    {
        public static WinnerParams GetPlayerHandRank(List<Card> inputCardList)
        {
            inputCardList.Sort();

            List<List<Card>> equalCardValueLists = MakeEqualCardValueOrSuitLists(inputCardList, CardParams.Value);
            List<List<Card>> equalCardSuitLists = MakeEqualCardValueOrSuitLists(inputCardList, CardParams.Suit);

            var isRoyalFlush = CheckForRoyalFlush(equalCardSuitLists);
            if (isRoyalFlush)
            {
                return new WinnerParams
                {
                    HandRank = PokerHandRankings.RoyalFlush,
                    HighCard = HighCard(inputCardList)
                };
            }

            var straightFlush = StraightFlushHighCard(equalCardSuitLists);
            if (straightFlush != null)
            {

                return new WinnerParams
                {
                    HandRank = PokerHandRankings.StraightFlush,
                    HighCard = straightFlush
                };
            }

            var fourOfAKind = FourOfAKindHighCard(equalCardValueLists);
            if (fourOfAKind != null)
            {
                return new WinnerParams
                {
                    HandRank = PokerHandRankings.FourOfAKind,
                    HighCard = fourOfAKind
                };
            }

            var fullHouse = EqualPairsHighCard(equalCardValueLists, 1, 3) != null && EqualPairsHighCard(equalCardValueLists, 1, 2) != null;
            if (fullHouse)
            {
                return new WinnerParams
                {
                    HandRank = PokerHandRankings.FullHouse,
                    HighCard = EqualPairsHighCard(equalCardValueLists, 1, 3)
                };
            }

            var flush = HighCard(equalCardSuitLists);
            if (flush != null)
            {
                return new WinnerParams
                {
                    HandRank = PokerHandRankings.Flush,
                    HighCard = flush
                };
            }

            var straight = StraightHighCard(inputCardList);
            if (straight != null)
            {
                return new WinnerParams
                {
                    HandRank = PokerHandRankings.Straight,
                    HighCard = straight
                };
            }

            var threeOfAKind = EqualPairsHighCard(equalCardValueLists, 1, 3);
            if (threeOfAKind != null)
            {
                return new WinnerParams
                {
                    HandRank = PokerHandRankings.ThreeOfAKind,
                    HighCard = threeOfAKind
                };
            }

            var twoPairs = EqualPairsHighCard(equalCardValueLists, 2, 2);
            if (twoPairs != null)
            {
                return new WinnerParams
                {
                    HandRank = PokerHandRankings.TwoPair,
                    HighCard = twoPairs
                };
            }

            var pair = EqualPairsHighCard(equalCardValueLists, 1, 2);
            if (pair != null)
            {
                return new WinnerParams
                {
                    HandRank = PokerHandRankings.Pair,
                    HighCard = pair
                };
            }

            var isHighCard = HighCard(inputCardList);
            return new WinnerParams
            {
                HandRank = PokerHandRankings.HighCard,
                HighCard = HighCard(inputCardList)
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

        private static bool CheckForRoyalFlush(List<List<Card>> inputCardLists)
        {
            if (inputCardLists == null) return false;

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

            return match == 5;

            //for (int i = 0; i < inputCardLists.Count; i++)
            //{
            //    if (inputCardLists[i].Where(inputCardListCard => straightFlushList.Any(straightFlushListCard => straightFlushListCard.Value == inputCardListCard.Value)).Distinct(new CardComparer()).ToList().Count == 5)
            //    {
            //        return true;
            //    }
            //}
            //return false;  
        }

        public static int IncreasingSequenceCardsCount(List<Card> inputCardList)
        {
            int match = 1;
            int maxMatch = 0;
            inputCardList = inputCardList.Distinct(new CardComparer()).ToList();

            for (int i = 0; i < inputCardList.Count - 1; i++)
            {
                if ((int)inputCardList[i + 1].Value == ((int)inputCardList[i].Value + 1))
                {
                    match++;
                }
                else
                {
                    if (maxMatch < match)
                    {
                        maxMatch = match;
                        match = 1;
                    }
                }
            }

            return (match > maxMatch) ?  match : maxMatch;
        }

        public static int EqualSequenceCardsCount(List<Card> inputCardList)
        {
            int match = 1;

            for (int i = 0; i < inputCardList.Count - 1; i++)
            {
                if ((int)inputCardList[i].Value == ((int)inputCardList[i + 1].Value))
                {
                    match++;
                }
                else
                    match = 1;
            }

            return match;
        }

        public static Card StraightFlushHighCard(List<List<Card>> inputCardLists)
        {
            if (inputCardLists == null) return null;

            foreach (var equalCardsList in inputCardLists)
            {
                if (IncreasingSequenceCardsCount(equalCardsList) == 5)
                {
                    return HighCard(equalCardsList);
                }
            }

            return null;
        }

        public static Card EqualPairsHighCard(List<List<Card>> inputCardLists, int neededMatchCount, int neededCardCount)
        {
            if (inputCardLists == null) return null;

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

            return (match == neededMatchCount) ? highCard : null;
        }

        public static Card FourOfAKindHighCard(List<List<Card>> inputCardLists)
        {
            if (inputCardLists == null) return null;

            foreach (List<Card> equalCardsList in inputCardLists)
            {
                if (EqualSequenceCardsCount(equalCardsList) == 4)
                {
                    return HighCard(equalCardsList);
                }
            }

            return null;
        }

        public static Card StraightHighCard(List<Card> inputCardList)
        {
            List<Card> aceCardList = new List<Card>
            {
                new Card() { Value = (CardValue)2, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)3, Suit = CardSuit.Diamonds },
                new Card() { Value = (CardValue)4, Suit = CardSuit.Hearts },
                new Card() { Value = (CardValue)5, Suit = CardSuit.Spades },
                new Card() { Value = (CardValue)14, Suit = CardSuit.Diamonds },
            };

            //var query = (from inputCardListCard in inputCardList
            //            join aceCardListCard in aceCardList
            //            on inputCardListCard.Value equals aceCardListCard.Value
            //            select inputCardListCard).Distinct(new CardComparer()).ToList();

            //if (query.Count == 5)
            //{
            //    return HighCard(inputCardList);
            //}    

            if (inputCardList.Where(inputCardListCard => aceCardList.Any(aceCardListCard => aceCardListCard.Value == inputCardListCard.Value)).Distinct(new CardComparer()).ToList().Count == 5)
            {
                return HighCard(inputCardList);
            }

            if (IncreasingSequenceCardsCount(inputCardList) >= 5)
            {
                var prevCard = inputCardList.Last();
                var maxCard = inputCardList.Last();
                var straightCount = 0;

                for (int i = inputCardList.Count - 1; i >= 0; i--)
                {
                    if ((int)prevCard.Value == (int)inputCardList[i].Value + 1)
                    {
                        straightCount++;
                    }
                    else
                    {
                        if (prevCard.Value == inputCardList[i].Value)
                        {
                            continue;
                        }

                        straightCount = 0;
                        maxCard = inputCardList[i];
                    }

                    prevCard = inputCardList[i];

                    if (straightCount == 4)
                    {
                        return maxCard;
                    }
                }
            }

            return null;
        }

        public static Card HighCard(List<List<Card>> inputCardLists)
        {
            if (inputCardLists == null) return null;

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
            return inputCardList.Max();
        }

    }
}
