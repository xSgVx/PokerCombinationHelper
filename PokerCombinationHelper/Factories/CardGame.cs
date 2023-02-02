using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
