using System.Collections.Generic;
using System.Linq;

namespace Scripts.RatingSystem
{
    public class ScoreStorage
    {
        private List<ScoreDto> _scores = new List<ScoreDto>();

        public IEnumerable<ScoreDto> GetAllScores()
        {
            return _scores;
        }

        public bool TryAddScore(ScoreDto newScore)
        {
            if (_scores.Exists(score => score.Id == newScore.Id))
                return false;

            _scores.Add(newScore);
            return true;
        }

        public ScoreDto GetScore(string id)
        {
            return _scores.FirstOrDefault(player => player.Id == id);
        }

        public bool TryUpdateScore(ScoreDto score)
        {
            var playerFromStorage = GetScore(score.Id);
            if (playerFromStorage == null)            
                return false;
                
            playerFromStorage.Rating = score.Rating;
            return true;            
        }
    }
}