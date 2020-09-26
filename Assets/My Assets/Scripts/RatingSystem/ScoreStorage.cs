using System.Collections.Generic;

using UnityEngine;

namespace NeuroDerby.RatingSystem
{
    public class ScoreStorage<TKey, TScore>
    {
        private Dictionary<TKey, TScore> _scores = new Dictionary<TKey, TScore>();

        public IEnumerable<TScore> GetAllScores()
        {
            return _scores.Values;
        }

        public bool TryAddScore(TKey id, TScore score)
        {
            if (id == null || _scores.ContainsKey(id))
                return false;

            if (id is string strId && strId == string.Empty)
            {
                Debug.LogWarning($"{nameof(TryAddScore)} {nameof(id)} cannot be empty string");
                return false;
            }

            _scores.Add(id, score);
            return true;
        }

        public bool TryGetScore(TKey id, out TScore score)
        {
            return _scores.TryGetValue(id, out score);
        }

        public bool TryUpdateScore(TKey id, TScore score)
        {
            if (!_scores.ContainsKey(id))
                return false;

            _scores[id] = score;
            return true;
        }
    }
}