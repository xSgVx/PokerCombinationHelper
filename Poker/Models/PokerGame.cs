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
        public delegate void MessageHandler(string message);
        public event MessageHandler? MessagesHandler;

        private delegate void WinnerHandler();
        private event WinnerHandler? winnerCalculate;

        private const byte _maxPlayersCount = 6;
        private byte _initPlayersCount;
        private PokerDeck _pokerDeck;
        private Board _board;
        private ICollection<IPlayer> _players;
        private bool _readyForStart = false;

        public IEnumerable<IPlayer> Winners => _winners;
        private IEnumerable<IPlayer> _winners;

        public PokerGame() : base("Poker")
        {
            _pokerDeck = new();
            _players = new List<IPlayer>();
            _board = new Board();
            winnerCalculate += CalculateWinner;
        }

        public void StartGame()
        {
            if (!_readyForStart)
            {
                MessagesHandler?.Invoke("Не готово к запуску.\n" +
                    "Необходимо указать число игроков");
                return;
            }

            StartNewRound();
        }

        public void StartNewRound()
        {
            _board.Clear();
            _players.Clear();
            _pokerDeck.RefreshDeck();

            AddPlayers(_initPlayersCount);
            AddCardToBoard(3);
        }

        public void AddCardToBoard(int count = 1)
        {
            if (_board.Cards.Count < 5)
            {
                _board.AddCards(_pokerDeck.GetCardsFromDeck(count));
                MessagesHandler?.Invoke("Карты на столе:\n" + CollectionToString(_board.Cards, ", "));
                winnerCalculate?.Invoke();
            }
            else
            {
                MessagesHandler?.Invoke("Ошибка при добавлении кард на стол.\n" +
                    $"Текущее количество кард на столе: {_board.Cards.Count}.\n" +
                    "Максимальное количество кард на столе: 5");
            }
        }

        private void CalculateWinner()
        {
            _winners = PokerGameAssistant.Instance.GetWinner(_players, _board);

            if (_winners.Count() > 1)
                MessagesHandler?.Invoke("Победители:");
            else
                MessagesHandler?.Invoke("Победитель:");

            MessagesHandler?.Invoke(CollectionToString(_winners));
        }
        /*
        private ICollection<IPlayer> CreatePlayersCollection(int count)
        {
            var players = new List<IPlayer>();

            for (int i = 0; i < count; i++)
            {
                players.Add(CreateRandomPlayer());
            }

            return players;
        }
        */
        public void SetPlayersCount(byte count)
        {
            if (count < 0 || count > _maxPlayersCount)
            {
                MessagesHandler?.Invoke($"Максимальное количество игроков: {_maxPlayersCount}, " +
                    $"введите другое число");
            }
            else
            {
                MessagesHandler?.Invoke($"Участвует {count} игроков");

                _initPlayersCount = count;
            }

            _readyForStart = true;
        }

        private string CollectionToString<T>(IEnumerable<T> collection, string separator = "\n")
        {
            return String.Join(separator, collection.Select(x => x?.ToString()));
        }

        public void AddPlayers(int count = 1)
        {
            if (_players != null && _players.Count < _maxPlayersCount)
            {
                for (int i = 0; i < count; i++)
                {
                    var player = CreateRandomPlayer();
                    _players.Add(player);
                    MessagesHandler?.Invoke("Добавлен игрок: " + player.ToString());
                }
            }
            else
            {
                MessagesHandler?.Invoke("Ошибка при попытке добавления игрока.\n" +
                    $"Максимальное количество игроков: {_maxPlayersCount}");
            }
        }

        public void RemovePlayer()
        {
            if (_players != null && _players.Count > 0)
            {
                _players.Remove(_players.Single());
                winnerCalculate?.Invoke();
            }
            else
            {
                MessagesHandler?.Invoke("Ошибка при попытке удалить игрока.\n" +
                    $"Текущее количество игроков: {_players?.Count}");
            }
        }

        private Player CreateRandomPlayer()
        {
            return new Player(Guid.NewGuid().ToString(),
                   new Stack<ICard>(_pokerDeck.GetCardsFromDeck(2)));
        }



    }
}
