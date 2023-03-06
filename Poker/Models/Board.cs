using CardGameBase.Interfaces;
using CardGameBase;

namespace Poker.Models;

internal class Board : IBoard
{
    public ICollection<ICard> Cards => _cards;

    private ICollection<ICard> _cards;

    public Board(IEnumerable<ICard> cards)
    {
        this._cards = cards.ToList();
    }

    public Board()
    {
       this._cards = new List<ICard>();
    }

    public void AddCards(IEnumerable<ICard> cards)
    {
        if (_cards == null)
        {
            _cards = new List<ICard>();
        }

        foreach (var card in cards)
        {
            _cards.Add(card);
        }
    }

    public void Clear()
    {
        _cards.Clear();
    }
}