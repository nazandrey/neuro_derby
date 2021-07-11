using System.Collections.Generic;
using System.Linq;
using Glicko2;
using NeuroDerby.Core;
using NeuroDerby.FileOperations;

namespace NeuroDerby.RatingSystem.Glicko
{
    public class GlickoScoreStorage : SingletonUndestroyable<GlickoScoreStorage>, IScoreStorage<string, Player>
    {
        private const string PlayersFilePath = @"data.json";
        
        private RatingCalculator _calculator = new RatingCalculator();
        private List<Player> _players;
        private ScoreStorage<string, Player> _playerScoreStorage = new ScoreStorage<string, Player>();
        private GlickoScoreUpdater<string> _scoreUpdater;

        protected override void Awake()
        {
            base.Awake();
            _scoreUpdater = GlickoScoreUpdaterBuilder.Build(_playerScoreStorage);
            LoadPlayers();
        }

        public IEnumerable<KeyValuePair<string, Player>> GetAllScoresWithId()
        {
            return _playerScoreStorage.GetAllScoresWithId();
        }

        public IEnumerable<Player> GetAllScores()
        {
            return _playerScoreStorage.GetAllScores();
        }

        public bool TryAddScore(string id, Player score)
        {
            return _playerScoreStorage.TryAddScore(id, score);
        }

        public bool TryGetScore(string id, out Player score)
        {
            return _playerScoreStorage.TryGetScore(id, out score);
        }

        public bool TryUpdateScore(string id, Player score)
        {
            return _playerScoreStorage.TryUpdateScore(id, score);
        }

        private void LoadPlayers()
        {
            _players = new List<Player>();
            var savedPlayers = FileLoader.Load<List<PlayerDto>>(PlayersFilePath);
            if (savedPlayers != null)
                _players = savedPlayers.Select(savedPlayer => new Player(_calculator, savedPlayer)).ToList();

            foreach (var player in _players)
                _playerScoreStorage.TryAddScore(player.Name, player);
        }
    }
}