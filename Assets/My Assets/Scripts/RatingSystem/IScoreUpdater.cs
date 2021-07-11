using NeuroDerby.Game.EventDatas;

namespace NeuroDerby.RatingSystem
{
    public interface IScoreUpdater
    {
        void UpdateScore(GameOverEventData gameOverEventData);
    }
}