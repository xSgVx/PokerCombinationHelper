using CardGameBase;
using CardGameBase.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTests")]
namespace Poker.Models
{
    internal class PokerGameAssistant
    {
        private static readonly Lazy<PokerGameAssistant> lazy =
            new Lazy<PokerGameAssistant>(() => new PokerGameAssistant());

        public static PokerGameAssistant Instance { get { return lazy.Value; } }

        public (IEnumerable<IPlayer>, PokerCombinations) GetWinnersAndComboName(IEnumerable<IPlayer> players, IBoard board)
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

            return (winners, currentWinnerCombination);
        }
    }
}
