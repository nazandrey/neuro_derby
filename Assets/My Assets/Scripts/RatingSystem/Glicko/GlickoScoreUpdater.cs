using Glicko2;
using NeuroDerby.Game.EventDatas;
using NeuroDerby.Players;

namespace NeuroDerby.RatingSystem.Glicko
{
    public class GlickoScoreUpdater : IScoreUpdater
    {
        private IScoreStorage<string, Player> _scoreStorage;
        private IPlayerNumToIdConverter<string> _playerNumToIdConverter;
        private IPlayersSaver _playersSaver;
        private RatingCalculator _scoreCalculator;

        public GlickoScoreUpdater(IScoreStorage<string, Player> scoreStorage, 
            IPlayerNumToIdConverter<string> playerNumToIdConverter,
            IPlayersSaver playersSaver,
            RatingCalculator scoreCalculator)
        {
            _scoreStorage = scoreStorage;
            _playerNumToIdConverter = playerNumToIdConverter;
            _playersSaver = playersSaver;
            _scoreCalculator = scoreCalculator;
        }

        public void UpdateScore(GameOverEventData gameOverEventData)
        {
            var winnerName = _playerNumToIdConverter.Get(gameOverEventData.WinnerPlayerNum);
            var loserName = _playerNumToIdConverter.Get(gameOverEventData.LoserPlayerNum);

            Calculate(gameOverEventData.IsDraw, winnerName, loserName);

            _playersSaver.Save();
        }
        
        private void Calculate(bool isDraw, string winnerName, string loserName)
        {
            if (!TryGetRatingInfoByName(winnerName, out var winnerRatingInfo)
                && !TryCreatePlayerAndReturnRatingInfo(winnerName, out winnerRatingInfo))
                return;
            
            if (!TryGetRatingInfoByName(loserName, out var loserRatingInfo)
                && !TryCreatePlayerAndReturnRatingInfo(loserName, out loserRatingInfo))
                return;

            var results = new RatingPeriodResults();
            if (isDraw)
                results.AddDraw(winnerRatingInfo, loserRatingInfo);
            else
                results.AddResult(winnerRatingInfo, loserRatingInfo);
            
            _scoreCalculator.UpdateRatings(results);
        }

        private bool TryCreatePlayerAndReturnRatingInfo(string newPlayerName, out Rating ratingInfo)
        {
            ratingInfo = default;
            var newPlayer = new Player(new Rating(_scoreCalculator), newPlayerName);
            if (!_scoreStorage.TryAddScore(newPlayerName, newPlayer))
                return false;
            
            ratingInfo = newPlayer.RatingInfo;
            return true;
        }

        private bool TryGetRatingInfoByName(string winnerName, out Rating rating)
        {
            if (_scoreStorage.TryGetScore(winnerName, out var player))
            {
                rating = player?.RatingInfo;
                return rating != null;
            }

            rating = null;
            return false;
        }
    }
}