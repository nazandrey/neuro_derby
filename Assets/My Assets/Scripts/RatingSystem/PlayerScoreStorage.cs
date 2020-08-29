using Scripts.Glicko;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.RatingSystem
{
    public class PlayerScoreStorage
    {
        private List<PlayerDto> _playerScores = new List<PlayerDto>();

        public IEnumerable<PlayerDto> GetAllScores()
        {
            return _playerScores;
        }

        public bool TryAddScore(PlayerDto player)
        {
            if (_playerScores.Exists(playerScore => playerScore.Name == player.Name))
                return false;

            _playerScores.Add(player);
            return true;
        }

        public PlayerDto GetScore(string name)
        {
            return _playerScores.FirstOrDefault(player => player.Name == name);
        }

        public bool TryUpdateScore(PlayerDto player)
        {
            var playerFromStorage = GetScore(player.Name);
            if (playerFromStorage == null)            
                return false;
                
            playerFromStorage.Rating = player.Rating;
            return true;            
        }
    }
}