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

        public IEnumerable<IPlayer> GetWinner(IEnumerable<IPlayer> players, IBoard board)
        {
            //IPlayer winner;
            List<IPlayer> winners = new List<IPlayer>();
            PokerCombinations currentWinnerCombination = PokerCombinations.Draw;
            IList<ICard> currentWinnerCards = null;

            foreach (var player in players)
            {
                var curPlayerComboHelper = new CombinationHelper(player.Cards, board.Cards);

                if (currentWinnerCombination < curPlayerComboHelper.Combination)
                {
                    currentWinnerCombination = curPlayerComboHelper.Combination;
                    currentWinnerCards = curPlayerComboHelper.PlayerCards;
                    winners.Clear();
                    winners.Add(player);
                    continue;
                }

                if (currentWinnerCombination == curPlayerComboHelper.Combination) 
                {
                    if (curPlayerComboHelper.HasEqualCards(currentWinnerCards))
                    {
                        winners.Add(player);
                        continue;
                    }

                    if (curPlayerComboHelper.HasGreaterCard(currentWinnerCards))
                    {
                        winners.Clear();
                        winners.Add(player);
                    }
                }
            }

            return winners;
        }
    }
}
