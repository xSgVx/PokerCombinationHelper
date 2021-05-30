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
        //public static Card[] Deck = Card.GetDeck();
        //public static int PlayerCount;

        static void Main()
        {

            //Console.WriteLine("Введите количество участников =");
            //PlayerCount = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Всего игроков =", PlayerCount);

            //Card testCard = new Card();
            //testCard = Card.GetRandomCard();
            //Console.WriteLine(testCard);

            Card[] testDeck = Card.GetDeck();
            Console.WriteLine(testDeck);

        }

    }



    //public static Card[] HandCards = Card.GetCards(2);
    //public static Card[] BoardCards = Card.GetCards(3);


}