namespace CardGameBase;

public interface IPlayer
{
    public string Name { get; }
    public IEnumerable<ICard> Cards { get; }
}