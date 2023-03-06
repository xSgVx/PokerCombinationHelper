using System.ComponentModel;

namespace CardGameBase;

public interface ICard : IEquatable<ICard>
{
    public CardValue Value { get; }
    public CardSuit Suit { get; }
}

public enum CardValue
{
    [Description("2")] Two = 2,
    [Description("3")] Three = 3,
    [Description("4")] Four = 4,
    [Description("5")] Five = 5,
    [Description("6")] Six = 6,
    [Description("7")] Seven = 7,
    [Description("8")] Eight = 8,
    [Description("9")] Nine = 9,
    [Description("10")] Ten = 10,
    [Description("J")] Jack = 11,
    [Description("Q")] Queen = 12,
    [Description("K")] King = 13,
    [Description("A")] Ace = 14,
}

public enum CardSuit
{
    [Description("\u2665")] Hearts = 1,
    [Description("\u2666")] Diamonds = 2,
    [Description("\u2663")] Clubs = 3,
    [Description("\u2660")] Spades = 4,
}

