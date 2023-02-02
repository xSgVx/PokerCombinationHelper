using CardGameBase.Models;
using System.Collections.Immutable;

namespace CardGameBase;

public interface IPlayer
{
    public string Name { get; }
    public IEnumerable<ICard> Cards { get; }
}