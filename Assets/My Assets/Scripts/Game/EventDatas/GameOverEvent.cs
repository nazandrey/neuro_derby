using NeuroDerby.RatingSystem;

namespace NeuroDerby.Game.EventDatas
{
    public class GameOverEvent : IEvent<GameOverEventData>
    {
        public EntryPoint EntryPoint { private get; set; }
        public IScoreUpdater ScoreUpdater { private get; set; }

        public void Dispatch(GameOverEventData data = null)
        {
            if (EntryPoint)
                EntryPoint.OnGameOver();
            if (ScoreUpdater != null)
                ScoreUpdater.UpdateScore(data);
        }
    }
}