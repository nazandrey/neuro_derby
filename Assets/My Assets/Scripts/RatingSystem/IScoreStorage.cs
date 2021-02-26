using System.Collections.Generic;

namespace NeuroDerby.RatingSystem
{
    public interface IScoreStorage<TKey, TScore>
    {
        IEnumerable<KeyValuePair<TKey, TScore>> GetAllScoresWithId();
    }
}