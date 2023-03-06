using CardGameBase;
using CardGameBase.Models.Comparers;
using System.ComponentModel;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTests")]
namespace Poker.Models
{
    public enum PokerCombinations
    {
        [Description("Флеш рояль")] RoyalFlush = 10,
        [Description("Стрит флеш")] StraightFlush = 9,
        [Description("Карэ")] FourOfAKind = 8,
        [Description("Фулл хаус")] FullHouse = 7,
        [Description("Флеш")] Flush = 6,
        [Description("Стрит")] Straight = 5,
        [Description("Сет (Тройка)")] ThreeOfAKind = 4,
        [Description("Две пары")] TwoPair = 3,
        [Description("Пара")] Pair = 2,
        [Description("Старшая карта")] HighCard = 1,
        [Description("Ничья")] Draw = 0,
    }

    internal class CombinationHelper
    {
        public PokerCombinations Combination { get; private set; }
        public IEnumerable<ICard> WinnerCards { get; private set; } //по сути не нужно
        public IList<ICard> PlayerCards { get; private set; }

        private Dictionary<CardSuit, List<ICard>> _dictByCardSuit;
        private Dictionary<CardValue, List<ICard>> _dictByCardValue;
        private IEnumerable<ICard> _royalFlush;
        private IEnumerable<ICard> _straightFlush;
        private IEnumerable<ICard> _fourOfAKind;
        private IEnumerable<ICard> _fullHouse;
        private IEnumerable<ICard> _flush;
        private IEnumerable<ICard> _straight;
        private IEnumerable<ICard> _threeOfAKind;
        private IEnumerable<ICard> _twoPair;
        private IEnumerable<ICard> _pair;

        public CombinationHelper(IEnumerable<ICard> playerCards, IEnumerable<ICard> boardCards)
        {
            PlayerCards = new List<ICard>(playerCards);
            var cards = playerCards.Concat(boardCards);
            _dictByCardValue = GetSortedDictionaryByCardValue(cards);
            _dictByCardSuit = GetSortedDictionaryByCardSuit(cards);
            FindCombinationAndWinnerCards(cards);
        }

        //для тестов отдельных функций
        internal CombinationHelper(IEnumerable<ICard> cards)
        {
            _dictByCardValue = GetSortedDictionaryByCardValue(cards);
            _dictByCardSuit = GetSortedDictionaryByCardSuit(cards);
            FindCombinationAndWinnerCards(cards);
        }

        private void FindCombinationAndWinnerCards(IEnumerable<ICard> cards)
        {

            if (IsRoyalFlush(cards) && _royalFlush.Intersect(PlayerCards).Any())
            {
                WinnerCards = _royalFlush;
                Combination = PokerCombinations.RoyalFlush;
                return;
            }

            if (IsStraightFlush(cards) && _straightFlush.Intersect(PlayerCards).Any())
            {
                WinnerCards = _straightFlush;
                Combination = PokerCombinations.StraightFlush;
                return;
            }

            if (IsFourOfAKind(cards) && _fourOfAKind.Intersect(PlayerCards).Any())
            {
                WinnerCards = _fourOfAKind;
                Combination = PokerCombinations.FourOfAKind;
                return;
            }

            if (IsFullHouse(cards) && _fullHouse.Intersect(PlayerCards).Any())
            {
                WinnerCards = _fullHouse;
                Combination = PokerCombinations.FullHouse;
                return;
            }

            if (IsFlush(cards) && _flush.Intersect(PlayerCards).Any())
            {
                WinnerCards = _flush;
                Combination = PokerCombinations.Flush;
                return;
            }

            if (IsStraight(cards) && _straight.Intersect(PlayerCards).Any())
            {
                WinnerCards = _straight;
                Combination = PokerCombinations.Straight;
                return;
            }

            if (IsThreeOfAKind(cards) && _threeOfAKind.Intersect(PlayerCards).Any())
            {
                WinnerCards = _threeOfAKind;
                Combination = PokerCombinations.ThreeOfAKind;
                return;
            }

            if (IsTwoPair(cards) && _twoPair.Intersect(PlayerCards).Any())   
            {
                WinnerCards = _twoPair;
                Combination = PokerCombinations.TwoPair;
                return;
            }

            if (IsPair(cards) && _pair.Intersect(PlayerCards).Any())
            {
                WinnerCards = _pair;
                Combination = PokerCombinations.Pair;
                return;
            }

            WinnerCards = new[] { GetHighCard(PlayerCards) };
            Combination = PokerCombinations.HighCard;
        }

        private Dictionary<CardSuit, List<ICard>> GetSortedDictionaryByCardSuit(IEnumerable<ICard> cards)
        {
            var dict = new Dictionary<CardSuit, List<ICard>>();

            foreach (var card in cards)
            {
                if (!dict.ContainsKey(card.Suit))
                {
                    dict[card.Suit] = new List<ICard>();
                }

                dict[card.Suit].Add(card);
            }

            foreach (var kv in dict)
            {
                kv.Value.Sort(new CardValueComparer(OrderBy.Desc));
            }

            return dict;
        }

        private Dictionary<CardValue, List<ICard>> GetSortedDictionaryByCardValue(IEnumerable<ICard> cards)
        {
            var dict = new Dictionary<CardValue, List<ICard>>();

            foreach (var card in cards)
            {
                if (!dict.ContainsKey(card.Value))
                {
                    dict[card.Value] = new List<ICard>();
                }

                dict[card.Value].Add(card);
            }

            return dict.OrderByDescending(x => x.Key)
                       .ToDictionary(x => x.Key, x => x.Value);
        }

        internal ICard GetHighCard(IEnumerable<ICard> cards)
        {
            return cards.Max(new CardValueComparer());
        }

        private bool IsRoyalFlush(IEnumerable<ICard> cards)
        {
            if (!IsStraightFlush(cards))
                return false;

            var royalFlushCardList = new List<ICard>()
            {
                new Card(CardValue.Ace, CardSuit.Hearts),
                new Card(CardValue.King, CardSuit.Hearts),
                new Card(CardValue.Queen, CardSuit.Hearts),
                new Card(CardValue.Jack, CardSuit.Hearts),
                new Card(CardValue.Ten, CardSuit.Hearts)
            };

            var royalFlushPlayerCards = cards?.Distinct(new CardValueComparer())
                                              .Intersect(royalFlushCardList, new CardValueComparer());

            if (royalFlushPlayerCards?.Count() == 5)
            {
                _royalFlush = royalFlushPlayerCards;
                return true;
            }

            return false;
        }

        private bool IsStraightFlush(IEnumerable<ICard> cards)
        {
            if (!IsFlush(cards))
                return false;

            if (IsStraight(_flush))
            {
                _straightFlush = _flush;
                return true;
            }

            return false;
        }

        private bool IsStraight(IEnumerable<ICard> cards)
        {
            if (cards.Count() < 5)
                return false;

            var inputCardList = cards.ToList();
            inputCardList = inputCardList.Distinct(new CardValueComparer()).ToList();
            inputCardList.Sort(new CardValueComparer(OrderBy.Desc));

            for (int i = 0; i < inputCardList.Count;)
            {
                var matchList = new List<ICard>();
                matchList.Add(inputCardList[i]);

                int j = 1;
                while (inputCardList.Any(card => card.Value == inputCardList[i].Value - j))
                {
                    matchList.Add(inputCardList.First(card => card.Value == inputCardList[i].Value - j));
                    j++;
                }

                i += j;
                if (matchList.Count >= 5)
                {
                    _straight = matchList;
                    return true;
                }
            }

            return false;
        }

        private bool IsFourOfAKind(IEnumerable<ICard> cards)
        {
            var fourCards = TryGetOneValueCards(4);

            if (fourCards != null)
            {
                _fourOfAKind = fourCards;
                return true;
            }

            return false;
        }

        private bool IsFullHouse(IEnumerable<ICard> cards)
        {
            if (IsThreeOfAKind(cards) & IsPair(cards))
            {
                _fullHouse = _threeOfAKind.Concat(_pair);
                return true;
            }

            return false;
        }

        private bool IsFlush(IEnumerable<ICard> cards)
        {
            if (_flush != null)
                return true;

            var flushCards = TryGetOneSuitCards(5);

            if (flushCards != null)
            {
                _flush = flushCards;
                return true;
            }

            return false;
        }

        private bool IsThreeOfAKind(IEnumerable<ICard> cards)
        {
            if (_threeOfAKind != null)
                return true;

            var threeCards = TryGetOneValueCards(3);

            if (threeCards != null)
            {
                _threeOfAKind = threeCards;
                return true;
            }

            return false;
        }

        private bool IsPair(IEnumerable<ICard> cards)
        {
            if (_pair != null)
                return true;

            var twoCards = TryGetOneValueCards(2);

            if (twoCards != null)
            {
                _pair = twoCards;
                return true;
            }

            return false;
        }

        private bool IsTwoPair(IEnumerable<ICard> cards)
        {
            if (_pair == null)
                return false;

            var secondPair = TryGetOneValueCards(2);

            if (secondPair != null)
            {
                _twoPair = _pair.Concat(secondPair);
                return true;
            }

            return false;
        }

        private IEnumerable<ICard>? TryGetOneValueCards(int oneValueCardsCount)
        {
            if (_dictByCardValue.Any(kv => kv.Value.Count() >= oneValueCardsCount))
            {
                var valueAndCards = _dictByCardValue.First(kv => kv.Value.Count() >= oneValueCardsCount);
                _dictByCardValue.Remove(valueAndCards.Key);
                return valueAndCards.Value;
            }

            return null;
        }

        private IEnumerable<ICard>? TryGetOneSuitCards(int oneValueCardsCount)
        {
            if (_dictByCardSuit.Any(kv => kv.Value.Count() >= oneValueCardsCount))
            {
                var valueAndCards = _dictByCardSuit.First(kv => kv.Value.Count() >= oneValueCardsCount);
                _dictByCardSuit.Remove(valueAndCards.Key);
                return valueAndCards.Value;
            }

            return null;
        }

        internal bool HasEqualCards(IEnumerable<ICard> anotherWinnerCards)
        {
            var equalCards = PlayerCards.Intersect(anotherWinnerCards, new CardValueComparer());
            return (equalCards.Count() == 2);
        }

        internal bool HasGreaterCard(IList<ICard> anotherWinnerCards)
        {
            return (PlayerCards[0].Value > anotherWinnerCards[0].Value &&
                    PlayerCards[0].Value > anotherWinnerCards[1].Value) ||
                    (PlayerCards[1].Value > anotherWinnerCards[0].Value &&
                    PlayerCards[1].Value > anotherWinnerCards[1].Value);
        }
    }
}
