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

        public TRating GetScore(TKey id)
        {
            return _scores.ContainsKey(id) ? _scores[id] : default;
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