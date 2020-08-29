using System.Collections.Generic;
using System.Linq;

namespace Scripts.RatingSystem
{
    public class ScoreStorage
    {
        private Dictionary<string, double> _scores = new Dictionary<string, double>();

        public IEnumerable<double> GetAllScores()
        {
            return _scores.Values;
        }

        public bool TryAddScore(string id, double rating)
        {
            if (_scores.ContainsKey(id))
                return false;

            _scores.Add(id, rating);
            return true;
        }

        public double GetScore(string id)
        {
            return _scores[id];
        }

        public bool TryUpdateScore(string id, double rating)
        {
            if (!_scores.ContainsKey(id))            
                return false;

            _scores[id] = rating;
            return true;            
        }
    }
}