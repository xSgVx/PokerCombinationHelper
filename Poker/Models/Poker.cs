using CardGameBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Collections.Immutable;
using CardGameBase.Factories;
using Poker.Extensions;
using System.Reflection.Metadata.Ecma335;

namespace Poker.Models
{
    public class PokerGame : CardGame
    {
        private PokerDeck pokerDeck = new();
        private Board _board;
        private Stack<ICard> _deck;
        private List<Player> _players;


        public PokerGame() : base("Poker")
        {
            _deck = new Stack<ICard>(pokerDeck.Cards);
            _players = CreatePlayers();
        }
        
        /*
        public void Start(int playersCount)
        {
            for (int i = 0; i < playersCount; i++)
            {
                ImmutableList<Card>
                Player player
            }
        }
        */


        private List<Player> CreatePlayers(int count = 5)
        {
            var players = new List<Player>();

            for (int i = 0; i < count; i++)
            {
                players.Add(CreateRandomPlayer());
            }

            return players;
        }

        private Player CreateRandomPlayer()
        {
            return new Player(Guid.NewGuid().ToString(), new Stack<ICard>(_deck.PopRange(2)));
        }


      
    }
}
