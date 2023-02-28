using CardGameBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CardGameBase.Factories
{
    public abstract class CardDeck
    {
        private IList<ICard> _cards;
        private readonly int _initCardNum;
        private readonly int _lastCardNum;

        /// <summary>
        /// Создание колоды из стандартных кард автоматически,
        /// по номеру начальной и конечной карты.
        /// По умолчанию колода начинается с двойки, заканчивается тузом.
        /// </summary>
        /// <param name="initCardNum">Начальная карта с которой нужно сгенерировать колоду. По умолчанию 2</param>
        /// <param name="lastCardNum">Конечная карта по которой нужно сгенерировать колоду. По умолчанию 14(Туз)</param>
        public CardDeck(int initCardNum = 2, int lastCardNum = 14)
        {
            _initCardNum = initCardNum; 
            _lastCardNum = lastCardNum;
            _cards = new List<ICard>();

            CreateDeck(_initCardNum, _lastCardNum);
        }

        /// <summary>
        /// Создание колоды из карт вручную.
        /// </summary>
        public CardDeck(params ICard[] cards)
        {
            _cards = new List<ICard>(cards);
        }

        public void RefreshDeck()
        {
            _cards.Clear();
            CreateDeck(_initCardNum, _lastCardNum);
        }

        public void RefreshDeck(params ICard[] cards)
        {
            _cards.Clear();
            _cards = cards;
        }

        public IEnumerable<ICard> GetCardsFromDeck(int count)
        {
            ICollection<ICard> cardsFromReck = new List<ICard>();

            for (int i = 0; i < count; i++)
            {
                cardsFromReck.Add(_cards.First());
                _cards.Remove(_cards.First());
            }

            return cardsFromReck;
        }

        private void CreateDeck(int initCardNum, int lastCardNum)
        {
            for (int i = initCardNum; i <= lastCardNum; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    _cards.Add(new Card((CardValue)i, (CardSuit)j));
                }
            }

            ShuffleDeck(ref _cards);
        }

        /// <summary>
        /// Метод для перемешивания колоды
        /// </summary>
        /// <param name="cards"></param>
        public void ShuffleDeck(ref IList<ICard> cards)
        {
            var rnd = new Random();
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int x = rnd.Next(n + 1);
                (cards[n], cards[x]) = (cards[x], cards[n]);
            }
        }
    }
}
