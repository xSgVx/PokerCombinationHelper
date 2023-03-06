using CardGameBase;
using CardGameBase.Factories;
using Poker.Source;
using CardGameBase.Extensions;

namespace Poker.Models
{
    internal class PokerGame : CardGame
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
                MessagesHandler?.Invoke(Messages.NotReadyToLaunch);
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
                MessagesHandler?.Invoke(Messages.CardOnBoard + 
                    Helpers.CollectionToString(_board.Cards, ", "));
                winnerCalculate?.Invoke();
            }
            else
            {
                MessagesHandler?.Invoke(Messages.ErrorAddingBoardCards);
            }
        }

        private void CalculateWinner()
        {
            var winnersAndCombo = PokerGameAssistant.Instance.GetWinnersAndComboName(_players, _board);
            _winners = winnersAndCombo.Item1;

            if (_winners.Count() > 1)
                MessagesHandler?.Invoke(Messages.WinnersWithCombo + winnersAndCombo.Item2.GetDescription()) ;
            else
                MessagesHandler?.Invoke(Messages.WinnerWithCombo + winnersAndCombo.Item2.GetDescription());

            MessagesHandler?.Invoke(Helpers.CollectionToString(_winners));
        }

        public void SetPlayersCount(byte count)
        {
            if (count < 0 || count > _maxPlayersCount)
            {
                MessagesHandler?.Invoke(Messages.ErrorOnSettingPlayersCount);
            }
            else
            {
                MessagesHandler?.Invoke(Messages.PlayersCountInfo + count);

                _initPlayersCount = count;
            }

            _readyForStart = true;
        }

        public void AddPlayers(int count = 1)
        {
            if (_players != null && _players.Count < _maxPlayersCount)
            {
                for (int i = 0; i < count; i++)
                {
                    var player = CreateRandomPlayer();
                    _players.Add(player);
                    MessagesHandler?.Invoke(Messages.PlayerAdded + player.ToString());
                }
            }
            else
            {
                MessagesHandler?.Invoke(Messages.ErrorAddingPlayer + _maxPlayersCount);
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
                MessagesHandler?.Invoke(Messages.ErrorDeletingPlayer + _players?.Count);
            }
        }

        private Player CreateRandomPlayer()
        {
            return new Player(Guid.NewGuid().ToString(),
                   new Stack<ICard>(_pokerDeck.GetCardsFromDeck(2)));
        }
    }
}
