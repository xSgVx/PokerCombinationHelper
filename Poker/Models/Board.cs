using CardGameBase.Interfaces;
using CardGameBase;

namespace Poker.Models;

public class Board : IBoard
{ 
	public IEnumerable<ICard> Cards => _cards;

    private IEnumerable<ICard> _cards;

    public Board(IEnumerable<ICard> cards)
	{
		this._cards = cards;
	}

	public void AddCards(IEnumerable<ICard> cards)
	{
		this._cards = this._cards.Union(cards);
	}
}