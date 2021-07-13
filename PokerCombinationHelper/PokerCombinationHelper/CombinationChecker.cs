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
        public static Dictionary<List<Card>, int> CheckForEqualCardValueOrSuit(List<Card> cardList, string type)
        {
            cardList.Sort();

            Dictionary<List<Card>, int> equalCardDict = new Dictionary<List<Card>, int>();

            for (int i = 0; i < cardList.Count;)
            {
                List<Card> equalCardsArray = new List<Card>();

                if (type == "Value")
                {
                    equalCardsArray = cardList.FindAll(x => x.Value.Equals(cardList[i].Value));
                }
                else if (type == "Suit")
                {
                    equalCardsArray = cardList.FindAll(x => x.Suit.Equals(cardList[i].Suit));
                }
                else throw new Exception("Неверный тип свойства");

                if (equalCardsArray.Count > 1)
                    equalCardDict.Add(equalCardsArray, equalCardsArray.Count);

                i += equalCardsArray.Count;
            }

            if (equalCardDict.Any()) return equalCardDict;
            else return null;
        }

        public static bool CheckForRoyalFlush(Dictionary<List<Card>, int> cardDict)
        {
            List<Card> straightFlushList = new List<Card>()
            {
                new Card() { Value = (CardValue)14, Suit = (CardSuit)1 },
                new Card() { Value = (CardValue)13, Suit = (CardSuit)2 },
                new Card() { Value = (CardValue)12, Suit = (CardSuit)3 },
                new Card() { Value = (CardValue)11, Suit = (CardSuit)4 },
                new Card() { Value = (CardValue)10, Suit = (CardSuit)1 },
            };

            int match = 0;
            foreach (KeyValuePair<List<Card>, int> keyValuePair in cardDict)
            {
                //Стрит флеш всегда содержит в себе 5 карт
                if (keyValuePair.Key.Count < 5) return false;

                foreach (Card card in straightFlushList)
                {
                    match += keyValuePair.Key.FindAll(x => x.Value.Equals(card.Value)).Count;
                }
            }

            if (match == 5) return true;
            else return false;
        }

        public static bool CheckForStraightFlush(Dictionary<List<Card>, int> cardDict)
        {
            int match = 0;

            foreach (KeyValuePair<List<Card>, int> keyValuePair in cardDict)
            {
                for (int i = 0; i < keyValuePair.Key.Count - 1; i++)
                {
                    if ((int)keyValuePair.Key[i].Value == ((int)keyValuePair.Key[i + 1].Value + 1))
                        match++;
                }
            }
            //Т.к 5 элементов и цикл идет до предпоследнего элемента
            if (match == 4) return true;
            else return false;
        }




    }
}
