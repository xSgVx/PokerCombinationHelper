using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PokerCombinationHelper
{
    public enum CardSuit
    {
        [Description("Черви")] Hearts = 1,
        [Description("Буби")] Diamonds = 2,
        [Description("Крести")] Clubs = 3,
        [Description("Пики")] Spades = 4,

    }
    public enum CardValue
    {
        [Description("Двойка")] Two = 2,
        [Description("Тройка")] Three = 3,
        [Description("Четверка")] Four = 4,
        [Description("Пятерка")] Five = 5,
        [Description("Шестерка")] Six = 6,
        [Description("Семерка")] Seven = 7,
        [Description("Восьмерка")] Eight = 8,
        [Description("Девятка")] Nine = 9,
        [Description("Десятка")] Ten = 10,
        [Description("Валет")] Jack = 11,
        [Description("Дама")] Queen = 12,
        [Description("Король")] King = 13,
        [Description("Туз")] Ace = 14,
    }
    public class Card
    {
        public CardValue Value;
        public CardSuit Suit;

        public static Card GetRandomCard()
        {
            var rnd = new Random();
            return new Card { Value = (CardValue)rnd.Next(2, 14), Suit = (CardSuit)rnd.Next(1, 4) };
        }

        public static Card[] GetCards(int count)
        {
            Card[] boardCards = new Card[count];

            for (int i = 0; i < boardCards.Length; i++)
            {
                boardCards[i] = Card.GetRandomCard();
            }

            return boardCards;
        }


    }
}



    //public class Card
    //{
    //    public Card(int value, int suit)
    //    {
    //        var rnd = new Random();

    //        Value = value = rnd.Next(2, 14);
    //        Suit = suit = rnd.Next(1, 4);
    //    }
    //    public readonly int Value;
    //    public readonly int Suit;

    //}

