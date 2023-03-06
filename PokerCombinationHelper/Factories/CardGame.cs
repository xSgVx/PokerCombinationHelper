namespace CardGameBase.Factories
{
    public abstract class CardGame
    {
        public string Name { get; protected set; }

        public CardGame(string s)
        {
            this.Name = s;
        }
    }
}
