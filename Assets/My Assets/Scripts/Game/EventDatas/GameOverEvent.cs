using NeuroDerby.RatingSystem;
using Zenject;

namespace NeuroDerby.Game.EventDatas
{
    public class GameOverEvent : IEvent<GameOverEventData>
    {
        private readonly IScoreUpdater _scoreUpdater;

        public GameOverEvent(IScoreUpdater scoreUpdater)
        {
            _scoreUpdater = scoreUpdater;
        }

        public EntryPoint EntryPoint { private get; set; }

        public void Dispatch(GameOverEventData data = null)
        {
            if (EntryPoint)
                EntryPoint.OnGameOver();
            if (_scoreUpdater != null)
                _scoreUpdater.UpdateScore(data);
        }
        
        public class Factory : PlaceholderFactory<GameOverEvent>
        {
        }
    }
}