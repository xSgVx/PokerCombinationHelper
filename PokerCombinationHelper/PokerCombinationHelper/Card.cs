﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;


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
    public class Card : IComparable<Card>
    {
        public CardValue Value;
        public CardSuit Suit;
        public static List<Card> Deck = GetDeck();

        //Объявляем колоду из 52 карт 
        public static List<Card> GetDeck()
        {
            List<Card> deckList = new List<Card>();
            int k = 0;

            for (int i = 2; i <= 14; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    Card card1 = new Card
                    {
                        Suit = (CardSuit)j,
                        Value = (CardValue)i
                    };
                    deckList.Add(card1);
                    k++;
                }
            }

            // В следствии многократного тестирования было замечено, что
            // метод GetCards выдает подряд идущие карты несмотря на random, 
            // поэтому был добавлен данный блок, который перемешивает
            // список (колоду карт) в случайном порядке
            var rnd = new Random();
            int n = deckList.Count;
            while (n > 1)
            {
                n--;
                int x = rnd.Next(n + 1);
                Card value = deckList[x];
                deckList[x] = deckList[n];
                deckList[n] = value;
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

        public int CompareTo(Card obj)
        {
            if (this.Value < obj.Value)
                return 1;
            if (this.Value > obj.Value)
                return -1;
            else
                return 0;
        }

        public class Cards : IEnumerable
        {
            private Card[] _card;
            public Cards(Card[] pArray)
            {
                _card = new Card[pArray.Length];

                for (int i = 0; i < pArray.Length; i++)
                {
                    _card[i] = pArray[i];
                }
            }

            // Implementation for the GetEnumerator method.
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public CardsEnum GetEnumerator()
            {
                return new CardsEnum(_card);
            }

        }
    }
    public class CardsEnum : IEnumerator
    {
        public Card[] _card;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;

        public CardsEnum(Card[] list)
        {
            _card = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _card.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Card Current
        {
            get
            {
                try
                {
                    return _card[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
