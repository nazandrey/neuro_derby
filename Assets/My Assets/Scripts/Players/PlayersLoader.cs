using System.Collections.Generic;
using System.Linq;
using Glicko2;
using NeuroDerby.RatingSystem;
using NeuroDerby.RatingSystem.Glicko;

namespace NeuroDerby.Players
{
    public class PlayersLoader : IPlayersLoader
    {
        private RatingCalculator _calculator;
        private IScoreStorage<string, Player> _playerScoreStorage;
        private IPlayersDtoLoader _playersDtoLoader;

        public PlayersLoader(RatingCalculator calculator, IScoreStorage<string, Player> playerScoreStorage,
            IPlayersDtoLoader playersDtoLoader)
        {
            _calculator = calculator;
            _playersDtoLoader = playersDtoLoader;
            _playerScoreStorage = playerScoreStorage;
        }

        public void Load()
        {
            var players = new List<Player>();
            var savedPlayers = _playersDtoLoader.Load();
            if (savedPlayers != null)
                players = savedPlayers.Select(savedPlayer => new Player(_calculator, savedPlayer)).ToList();

            foreach (var player in players)
                _playerScoreStorage.TryAddScore(player.Name, player);
        }
    }
}