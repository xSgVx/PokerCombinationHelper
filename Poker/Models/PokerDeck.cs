using CardGameBase;
using CardGameBase.Factories;
using CardGameBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Models
{
    internal class PokerDeck : CardDeck
    {
        public PokerDeck() : base(2,14)
        {
        
        }
    }
}
