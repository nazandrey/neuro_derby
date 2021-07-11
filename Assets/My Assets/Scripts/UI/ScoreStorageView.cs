using System;
using System.Linq;
using NeuroDerby.RatingSystem;
using NeuroDerby.RatingSystem.Glicko;
using UnityEngine;

namespace NeuroDerby.UI
{
    public class ScoreStorageView : MonoBehaviour
    {
        [SerializeField] 
        private PlayerScoreView playerScoreViewPrefab;
        
        private IScoreStorage<string, Player> _playerScoreStorage;

        private void Awake()
        {
            _playerScoreStorage = GlickoScoreStorage.Instance;
        }

        private void Start()
        {
            var idAndScores = _playerScoreStorage.GetAllScoresWithId()
                .OrderByDescending(idAndScore => idAndScore.Value);
            var place = 1;
            foreach (var idAndScore in idAndScores)
            {
                var playerScoreView = Instantiate(playerScoreViewPrefab, transform, false);
                playerScoreView.Init(place, idAndScore.Key, idAndScore.Value.Rating);
                place++;
            }
        }
    }
}