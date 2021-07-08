using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cards;
using Checker;

namespace PokerCombinationHelper
{
    class Program
    {
        static void Main()
        {

            List<Card> randomHand = Card.GetCards(5);

            randomHand.Sort();

            if (CombinationChecker.OnePair(randomHand))
                Console.WriteLine ("есть пара");


        }

    }
}
