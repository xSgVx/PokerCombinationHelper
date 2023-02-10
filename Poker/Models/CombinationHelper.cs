using CardGameBase;
using CardGameBase.Interfaces;
using CardGameBase.Models.Comparers;
using Poker.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
        private static readonly Lazy<CombinationHelper> lazy =
            new Lazy<CombinationHelper>(() => new CombinationHelper());

        private Dictionary<CardSuit, List<ICard>> _dictByCardSuit = new();
        private Dictionary<CardValue, List<ICard>> _dictByCardValue = new();

        public static CombinationHelper Instance { get { return lazy.Value; } }

        public Player? GetWinner(IList<IPlayer> players, IBoard board)
        {
            Player winner = null;
            PokerCombinations winnerCombo = PokerCombinations.Draw;

            foreach (var player in players)
            {
                var allCards = player.Cards.Union(board.Cards);
                _dictByCardSuit = GetSortedDictionaryByCardSuit(allCards);
                _dictByCardValue = GetSortedDictionaryByCardValue(allCards);
                var playerCombo = GetPlayerCombination(allCards);



            }

            return winner;
        }

        internal PokerCombinations GetPlayerCombination(IEnumerable<ICard> cards)
        {
            if (IsRoyalFlush(cards)) return PokerCombinations.RoyalFlush;
            if (IsStraightFlush(cards)) return PokerCombinations.StraightFlush;

            if (IsStraight(cards)) return PokerCombinations.Straight;


            return PokerCombinations.Draw;
        }

        internal bool IsStraightFlush(IEnumerable<ICard> cards)
        {
            if (cards.Count() < 5) return false;    //только 5 карт

            if (TryGetStraightCombo(cards, true) != null)
                return true;

            return false;
        }

        internal Dictionary<CardSuit, List<ICard>> GetSortedDictionaryByCardSuit(IEnumerable<ICard> cards)
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

        internal Dictionary<CardValue, List<ICard>> GetSortedDictionaryByCardValue(IEnumerable<ICard> cards)
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

            return dict;
        }

        internal bool IsStraight(IEnumerable<ICard> cards)
        {
            if (cards.Count() < 5) return false;

            if (TryGetStraightCombo(cards, false) != null)
                return true;

            return false;
        }


        private IEnumerable<ICard>? TryGetStraightCombo(IEnumerable<ICard> cards, bool needCheckSuit)
        {
            if (cards.Count() < 5)
                return null;

            if (needCheckSuit)
            {
                //if (!_dictByCardSuit.Any())  с юнит тестами ошибка
                _dictByCardSuit = GetSortedDictionaryByCardSuit(cards);

                if (_dictByCardSuit.Any(kv => kv.Value.Count >= 5))
                {
                    foreach (var suitAndCards in _dictByCardSuit)
                    {
                        if (suitAndCards.Value.Count < 5) continue;

                        return GetSequenceCardsList(suitAndCards.Value);
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return GetSequenceCardsList(cards);
            }


            return null;
        }



        private IEnumerable<ICard>? GetSequenceCardsList(IEnumerable<ICard> cards)
        {
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
                if (matchList.Count == 5)
                {
                    return matchList;
                }
            }

            return null;
        }

        internal bool IsRoyalFlush(IEnumerable<ICard> cards)
        {
            if (!IsStraightFlush(cards)) return false;

            var royalFlushCardList = new List<ICard>()
            {
                new Card(CardValue.Ace, CardSuit.Hearts),
                new Card(CardValue.King, CardSuit.Hearts),
                new Card(CardValue.Queen, CardSuit.Hearts),
                new Card(CardValue.Jack, CardSuit.Hearts),
                new Card(CardValue.Ten, CardSuit.Hearts)
            };

            var straightFlushCombo = TryGetStraightCombo(cards, true);

            var royalFlushPlayerCards = straightFlushCombo?.Distinct(new CardValueComparer())
                                                           .ToList()
                                                           .Select(x => x)
                                                           .Intersect(royalFlushCardList, new CardValueComparer());

            if (royalFlushPlayerCards?.Count() == 5) return true;

            return false;
        }

        //internal ICard GetHighCard(IPlayer player, IBoard board)
        //{
        //
        //
        //    return null;
        //}





    }
}
