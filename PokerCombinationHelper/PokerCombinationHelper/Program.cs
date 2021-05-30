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

            List<Card> rnd4Cards = Card.GetCards(1);

            Console.WriteLine(rnd4Cards);

        }
    }
}