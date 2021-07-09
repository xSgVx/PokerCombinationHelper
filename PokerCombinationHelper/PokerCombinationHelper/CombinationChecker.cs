using Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;
using System.Collections;

namespace Checker
{
    public class CombinationChecker
    {
        public static Dictionary<List<Card>, int> CheckForEqualCardValue(List<Card> cardList, string type)
        {
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
    }
}
