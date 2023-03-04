using CardGameBase;
using System.Collections.Immutable;

namespace Poker.Models;

public class Player : IPlayer
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
                          $"cards: {String.Join(", ", 
                          this.Cards.Select(card => card.ToString()))}");
    }
}