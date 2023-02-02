using CardGameBase.Models;

namespace CardGameBase.Interfaces
{
    public interface IBoard
    {
        public IEnumerable<ICard> Cards { get; }
    }
}
