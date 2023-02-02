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
        public IEnumerable<ICard> Cards => _cards;
        private IEnumerable<ICard> _cards = Enumerable.Empty<ICard>();
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

            CreateDeck(_initCardNum, _lastCardNum);
        }

        /// <summary>
        /// Создание колоды из карт вручную.
        /// </summary>
        public CardDeck(params ICard[] cards)
        {
            _cards = _cards.Union(cards);
        }

        public void RefreshDeck()
        {
            CreateDeck(_initCardNum, _lastCardNum);
        }

        public void RefreshDeck(params ICard[] cards)
        {
            _cards = Enumerable.Empty<ICard>(); 
            _cards = _cards.Union(cards);
        }

        private void CreateDeck(int initCardNum, int lastCardNum)
        {
            var deck = new List<ICard>();

            for (int i = initCardNum; i <= lastCardNum; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    deck.Add(new Card((CardValue)i, (CardSuit)j));
                }
            }

            _cards = ShuffleDeck(deck);
        }

        /// <summary>
        /// Метод для перемешивания колоды
        /// </summary>
        /// <param name="cards"></param>
        public IEnumerable<ICard> ShuffleDeck(IEnumerable<ICard> cards)
        {
            List<ICard> shuffledDeck = new(cards);

            var rnd = new Random();
            int n = cards.Count();
            while (n > 1)
            {
                n--;
                int x = rnd.Next(n + 1);
                (shuffledDeck[n], shuffledDeck[x]) = (shuffledDeck[x], shuffledDeck[n]);
            }

            return shuffledDeck;
        }
    }
}
