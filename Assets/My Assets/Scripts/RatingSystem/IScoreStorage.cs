using System.Collections.Generic;

namespace NeuroDerby.RatingSystem
{
    public interface IScoreStorage<TKey, TScore>
    {
        IEnumerable<KeyValuePair<TKey, TScore>> GetAllScoresWithId();
        IEnumerable<TScore> GetAllScores();
        bool TryAddScore(TKey id, TScore score);
        bool TryGetScore(TKey id, out TScore score);
        bool TryUpdateScore(TKey id, TScore score);
    }
}