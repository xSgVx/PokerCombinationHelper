using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PokerCombinationHelper;
using Checker;

namespace Cards
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
        public static List<Card> Deck = Card.GetDeck();
        
        //Объявляем колоду из 52 карт 
        public static List<Card> GetDeck()
        {
            List<Card> deckList = new List<Card>();
            int k = 0;

            for (int i = 2; i <= 14; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    Card card1 = new Card();
                    card1.Suit = (CardSuit)j;
                    card1.Value = (CardValue)i;
                    deckList.Add(card1);
                    k++;
                }
            }
            return deckList;
        }


        public static List<Card> GetCards(int count)
        {
            List<Card> randomedCardList = new List<Card>();

            for (int i = 0; i < count; i++)
            {
                var rnd = new Random();
                int rndIndexRandomedCard = rnd.Next(0, Deck.Count() - 1);

                Card randomedCard = Card.Deck[rndIndexRandomedCard];

                randomedCardList.Add(randomedCard);

                Deck.RemoveAt(rndIndexRandomedCard);
            }


            return randomedCardList;
        }


    }

}
