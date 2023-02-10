using CardGameBase.Models;

namespace CardGameBase.Interfaces
{
    public interface IBoard
    {
        public ICollection<ICard> Cards { get; }
    }
}
