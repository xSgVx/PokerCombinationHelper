using CardGameBase;
using System.Collections.Immutable;

namespace Poker.Models;

public class Player : IPlayer
{
    public string Name { get; }
    public IEnumerable<ICard> Cards { get; }

    public Player(string name, Stack<ICard> cards)
    {
        this.Name = name;
        this.Cards = cards;
    }
}