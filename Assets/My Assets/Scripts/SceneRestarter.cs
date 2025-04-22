using NeuroDerby.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace NeuroDerby
{
    public class SceneRestarter : MonoBehaviour
    {
        [SerializeField]
        private Button restartButton;

        private GameState _gameState;
        
        [Inject]
        private void Construct(GameState gameState)
        {
            _gameState = gameState;
        }
        
        private void Start()
        {
            _gameState.SetGameIsOver(false);
            if (restartButton)
                restartButton.onClick.AddListener(() => SceneManager.LoadScene(SceneName.Game.ToString()));
        }
    }
}