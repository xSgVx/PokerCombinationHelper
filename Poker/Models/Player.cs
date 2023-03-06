using CardGameBase;
using Poker.Source;

namespace Poker.Models;

internal class Player : IPlayer
{
    public string Name { get; }
    public IEnumerable<ICard> Cards { get; }

    public Player(string name, IEnumerable<ICard> cards)
    {
        this.Name = name;
        this.Cards = cards;
    }

    public override string ToString()
    {
        return new string($"player: {this.Name}, " +
                          $"cards: " + Helpers.CollectionToString(this.Cards, ", "));
    }

}