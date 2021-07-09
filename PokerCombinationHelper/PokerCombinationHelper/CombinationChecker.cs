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

        public static Dictionary<List<Card>, int> CheckForEqualCardValue(List<Card> cardList)
        {
            Dictionary<List<Card>, int> equalCardsDict = new Dictionary<List<Card>, int>();

            for (int i = 0; i < cardList.Count - 1;)
            {
                List<Card> equalCardsArray = new List<Card>();

                equalCardsArray = cardList.FindAll(x => x.Value.Equals(cardList[i].Value));
                
                equalCardsDict.Add(equalCardsArray, equalCardsArray.Count);

                i += equalCardsArray.Count;
            }

            if (equalCardsDict.Any()) return equalCardsDict;
            else return null;
        }
    }
}
