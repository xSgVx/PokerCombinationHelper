using CardGameBase.Interfaces;
using CardGameBase;

namespace Poker.Models;

public class Board : IBoard
{
    public ICollection<ICard> Cards => _cards;

    private ICollection<ICard> _cards;

    public Board(IEnumerable<ICard> cards)
    {
        this._cards = cards.ToList();
    }

    public void AddCards(IEnumerable<ICard> cards)
    {
        foreach (var card in cards)
        {
            _cards.Add(card);
        }
        //this._cards.Union(cards);
    }
}