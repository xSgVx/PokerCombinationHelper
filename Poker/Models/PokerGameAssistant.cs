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
    internal class PokerGameAssistant
    {
        private static readonly Lazy<PokerGameAssistant> lazy =
            new Lazy<PokerGameAssistant>(() => new PokerGameAssistant());

        public static PokerGameAssistant Instance { get { return lazy.Value; } }

        public IEnumerable<IPlayer> GetWinner(IList<IPlayer> players, IBoard board)
        {
            //IPlayer winner;
            List<IPlayer> winners = new List<IPlayer>();
            PokerCombinations currentWinnerCombination = PokerCombinations.Draw;
            IEnumerable<ICard> currentWinnerCards = null;

            foreach (var player in players)
            {
                var allCards = player.Cards.Concat(board.Cards);

                var comboHelper = new CombinationHelper(player.Cards, board.Cards);

                if (currentWinnerCombination < comboHelper.Combination)
                {
                    currentWinnerCombination = comboHelper.Combination;
                    currentWinnerCards = comboHelper.WinnerCards;
                    winners.Clear();
                    winners.Add(player);
                    continue;
                }

                if (currentWinnerCombination == comboHelper.Combination) 
                {
                    var secondWinner = comboHelper.TryGetSecondWinnerHighCard(currentWinnerCards);

                    if (secondWinner == null)   //несколько победителей с одинаковой highcard
                    {
                        winners.Add(player);
                    }
                    else    //победитель один
                    {
                        winners.Clear();
                        winners.Add(secondWinner);
                    }
                }
            }

            return winners;
        }





    }
}
