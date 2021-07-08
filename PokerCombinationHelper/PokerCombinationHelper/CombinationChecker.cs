using Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;

namespace Checker
{
    public class CombinationChecker
    {
        public static bool OnePair(List<Card> cardList)
        {
            for (int i = 0; i < cardList.Count-1; i++)
            {
                if (cardList[i].Value == cardList[i + 1].Value) return true;
            }
            
            return false;
        }

        public static bool TwoPairs(List<Card> cardList)
        {
            int match = 0;
            foreach(Card card in cardList)
            {
                int matchCardValue = 0;
                matchCardValue += cardList.FindAll(x => x.Value.Equals(card.Value)).Count;
                if (matchCardValue == 2) match++;
            }

            if (match == 4) return true;
            else return false;
        }

    }
}
