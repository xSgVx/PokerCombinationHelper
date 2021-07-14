using Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Checker
{
    public class CombinationChecker
    {
        public static List<List<Card>> CheckForEqualCardValueOrSuit(List<Card> inputCardList, string type)
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
                    equalCardList.Add(equalCardsArray);                    

                //Смещаем индекс массива на количество найденных элементов
                i += equalCardsArray.Count;
            }

            if (equalCardList.Any()) return equalCardList;
            else return null;
        }

        public static bool CheckForRoyalFlush(List<List<Card>> inputCardList)
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
                if (inputCardList[i].Count < 5) return false;

                foreach (Card card in straightFlushList)
                {
                    match += inputCardList[i].FindAll(x => x.Value.Equals(card.Value)).Count;
                }
            }

            if (match == 5) return true;
            else return false;
        }

        public static bool CheckForStraightFlush(List<List<Card>> inputCardList)
        {
            int match = 0;

            foreach (List<Card> equalCardsList in inputCardList)
            {
                for (int i = 0; i < equalCardsList.Count - 1; i++)
                {
                    if ((int)equalCardsList[i].Value == ((int)equalCardsList[i + 1].Value + 1))
                        match++;
                }
            }
            //Т.к 5 элементов и цикл идет до предпоследнего элемента
            if (match == 4) return true;
            else return false;
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



    }
}
