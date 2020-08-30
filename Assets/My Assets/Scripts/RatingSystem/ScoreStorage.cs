using System.Collections.Generic;

namespace Scripts.RatingSystem
{
    public class ScoreStorage<TKey, TRating>
    {
        private Dictionary<TKey, TRating> _scores = new Dictionary<TKey, TRating>();

        public IEnumerable<TRating> GetAllScores()
        {
            return _scores.Values;
        }

        public bool TryAddScore(TKey id, TRating rating)
        {
            if (_scores.ContainsKey(id))
                return false;

            _scores.Add(id, rating);
            return true;
        }

        public bool TryGetScore(TKey id, out TRating score)
        {
            score = default;
            if (!_scores.ContainsKey(id))
                return false;

            score = _scores[id];
            return true;
        }

        public bool TryUpdateScore(TKey id, TRating rating)
        {
            if (!_scores.ContainsKey(id))            
                return false;

            _scores[id] = rating;
            return true;            
        }
    }
}