using Glicko2;
using NeuroDerby.Game;
using NeuroDerby.Game.EventDatas;
using NeuroDerby.Players;
using UnityEngine;

namespace NeuroDerby.RatingSystem.Glicko
{
    public class GlickoScoreUpdater<TPlayerId> : IScoreUpdater
    {
        private IScoreStorage<TPlayerId, Player> _scoreStorage;
        private IPlayerNumToIdConverter<TPlayerId> _playerNumToIdConverter;
        private RatingCalculator _scoreCalculator;
        private IPlayersSaver _playersSaver;

        public GlickoScoreUpdater(IScoreStorage<TPlayerId, Player> scoreStorage, 
            IPlayerNumToIdConverter<TPlayerId> playerNumToIdConverter,
            IPlayersSaver playersSaver,
            RatingCalculator scoreCalculator)
        {
            _scoreCalculator = scoreCalculator;
            _scoreStorage = scoreStorage;
            _playerNumToIdConverter = playerNumToIdConverter;
            _playersSaver = playersSaver;
        }

        public void UpdateScore(GameOverEventData gameOverEventData)
        {
            var winnerName = _playerNumToIdConverter.Get(gameOverEventData.WinnerPlayerNum);
            var loserName = _playerNumToIdConverter.Get(gameOverEventData.LoserPlayerNum);
            
            Calculate(gameOverEventData.IsDraw, winnerName, loserName);

            _playersSaver.Save();
        }
        
        private void Calculate(bool isDraw, TPlayerId winnerName, TPlayerId loserName)
        {
            if (!TryGetRatingInfoByName(winnerName, out var winnerRatingInfo) || 
                !TryGetRatingInfoByName(loserName, out var loserRatingInfo))
            {
                Debug.Log($"Rating info not found for {winnerName} or {loserName}");
                return;
            }

            var results = new RatingPeriodResults();
            if (isDraw)
                results.AddDraw(winnerRatingInfo, loserRatingInfo);
            else
                results.AddResult(winnerRatingInfo, loserRatingInfo);
            _scoreCalculator.UpdateRatings(results);
        }

        private bool TryGetRatingInfoByName(TPlayerId winnerName, out Rating rating)
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