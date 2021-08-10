using System;
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
        public static List<List<Card>> MakeEqualCardValueOrSuitLists(List<Card> inputCardList, string type)
        {
            inputCardList.Sort();

            var equalCardList = new List<List<Card>>();

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
                    equalCardList.Add(equalCardsArray);
                }

                //Смещаем индекс массива на количество найденных элементов
                i += equalCardsArray.Count;
            }

            if (equalCardList.Any())
            {
                return equalCardList;
            }
            else
            {
                return null;
            }
        }

        private static bool CheckForRoyalFlush(List<List<Card>> inputCardList)
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

            for (int i = 0; i < inputCardList.Count; i++)
            {
                //Стрит флеш всегда содержит в себе 5 карт
                if (inputCardList[i].Count < 5)
                {
                    return false;
                }

                foreach (Card card in straightFlushList)
                {
                    match += inputCardList[i].FindAll(x => x.Value.Equals(card.Value)).Count;
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

        private static bool CheckForStraightFlush(List<List<Card>> inputCardList)
        {
            foreach (var equalCardsList in inputCardList)
            {
                if (CardSequenceCount(equalCardsList) == 4)
                {
                    return true;
                }
            }
            return false;
        }

        private static int CardSequenceCount(List<Card> inputCardList)
        {
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

        private static bool CheckFourOfAKind(List<List<Card>> inputCardList)
        {

            for (int i = 0; i < inputCardList[i].Count; i++)
            {
                //Проверка на 4 элемента в листе
                if (inputCardList[i].Count != 4)
                {
                    continue;
                }

                int match = 0;

                foreach (Card card in inputCardList[i])
                {
                    match += inputCardList[i].FindAll(x => x.Value.Equals(card.Value)).Count;
                }

                if (match == 4)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool CheckThreeOfAKind(List<List<Card>> inputCardList)
        {
            foreach (List<Card> equalCardsList in inputCardList)
            {
                if (CardSequenceCount(equalCardsList) == 3)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool CheckThreeOfAKind(List<Card> inputCardList)
        {
            if (CardSequenceCount(inputCardList) == 3)
            {
                return true;
            }

            return false;
        }

        private static bool CheckFullHouse(List<List<Card>> inputCardList)
        {
            if (CheckThreeOfAKind(inputCardList))
            {
                foreach (List<Card> equalCardsList in inputCardList)
                {
                    if (CardSequenceCount(equalCardsList) == 3)
                    {
                        continue;
                    }

                    if (CardSequenceCount(equalCardsList) == 2)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool CheckFlush(List<List<Card>> inputCardList)
        {
            foreach (List<Card> equalCardsList in inputCardList)
            {
                if (equalCardsList.Count == 5)
                {
                    return true;
                }
            }

            return false;
        }

        public static Card Kicker(List<List<Card>> inputCardList)
        {
            var kickerList = new List<Card>();

            foreach (List<Card> cardList in inputCardList)
            {
                cardList.Sort();
                kickerList.Add(cardList[0]);
            }

            kickerList.Sort();

            return kickerList[0];
        }

        public static Card Kicker(List<Card> inputCardList)
        {
            inputCardList.Sort();

            return inputCardList[0];
        }

        public static PokerHandRankings GetPlayerHandRank(List<Card> inputCardList)
        {

            var equalCardValueList = MakeEqualCardValueOrSuitLists(inputCardList, "Value");
            var equalCardSuitList = MakeEqualCardValueOrSuitLists(inputCardList, "Suit");

            //Флеш рояль
            if (CheckForRoyalFlush(equalCardSuitList))
            {
                return (PokerHandRankings)10;
            }

            //Стрит-флеш
            if (CheckForStraightFlush(equalCardSuitList))
            {
                return (PokerHandRankings)9;
            }

            //Каре
            if (CheckFourOfAKind(equalCardValueList))
            {
                return (PokerHandRankings)8;
            }

            //Фулл-хаус
            if (CheckFullHouse(equalCardValueList))
            {
                return (PokerHandRankings)7;
            }

            //Флеш
            if (CheckFlush(equalCardSuitList))
            {
                return (PokerHandRankings)6;
            }

            //Стрит
            if (CardSequenceCount(inputCardList) == 5)
            {
                return (PokerHandRankings)5;
            }

            //Тройка
            if (CheckThreeOfAKind(inputCardList))
            {
                return (PokerHandRankings)4;
            }

            //Две пары
            if (equalCardValueList.Count >= 2)
            {
                return (PokerHandRankings)3;
            }

            //Пара
            if (equalCardValueList.Count == 1)
            {
                return (PokerHandRankings)2;
            }

            return (PokerHandRankings)1;

        }
    }
}
