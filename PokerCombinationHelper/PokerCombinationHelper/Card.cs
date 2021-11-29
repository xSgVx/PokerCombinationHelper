using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace PokerCombinationHelper
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum enumElement)
        {
            Type type = enumElement.GetType();

            MemberInfo[] memInfo = type.GetMember(enumElement.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return enumElement.ToString();
        }
    }
    public enum CardSuit
    {
        [Description("\u2665")] Hearts = 1,
        [Description("\u2666")] Diamonds = 2,
        [Description("\u2663")] Clubs = 3,
        [Description("\u2660")] Spades = 4,
    }
    public enum CardValue
    {
        [Description("2")] Two = 2,
        [Description("3")] Three = 3,
        [Description("4")] Four = 4,
        [Description("5")] Five = 5,
        [Description("6")] Six = 6,
        [Description("7")] Seven = 7,
        [Description("8")] Eight = 8,
        [Description("9")] Nine = 9,
        [Description("10")] Ten = 10,
        [Description("J")] Jack = 11,
        [Description("Q")] Queen = 12,
        [Description("K")] King = 13,
        [Description("A")] Ace = 14,
    }
    public class Card : IComparable<Card>, IEquatable<Card>
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
                return -1;
            if (this.Value > obj.Value)
                return 1;
            else
                return 0;
        }

        public bool Equals(Card other)
        {
            if (other == null)
                return false;

            return (other.Suit == this.Suit) && (other.Value == this.Value);
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

    class CardComparer : EqualityComparer<Card>
    {
        public override bool Equals(Card x, Card y)
        {
            return (x.Value.Equals(y.Value));
        }

        public override int GetHashCode(Card obj)
        {
            return obj.Value.GetHashCode();
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
