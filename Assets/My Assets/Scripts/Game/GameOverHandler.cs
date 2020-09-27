using UnityEngine;
using UnityEngine.UI;

namespace NeuroDerby.Game
{
    public class GameOverHandler : MonoBehaviour
    {

        public Health player1Health;
        public Health player2Health;
        public Text playerWonText;
        public GameObject playerWonArea;

        public GameOverEvent GameOverEvent { get; private set; }

        private void Awake()
        {
            GameOverEvent = new GameOverEvent();
        }

        private void Start()
        {
            player1Health.OnDeathEvent.GameOverHandler = this;
            player2Health.OnDeathEvent.GameOverHandler = this;
        }

        public void OnDeathEvent()
        {
            if (player1Health.IsDead && player2Health.IsDead)
            {
                HandleGameOver("DRAW!");
            }
            else if (player2Health.IsDead)
            {
                HandleGameOver("Player 1 WON!");
            }
            else if (player1Health.IsDead)
            {
                HandleGameOver("Player 2 WON!");
            }
        }

        private void HandleGameOver(string gameOverText)
        {
            ShowPlayerGameOverText(gameOverText);
            GameOverEvent.Dispatch();
        }

        private void ShowPlayerGameOverText(string text)
        {
            playerWonText.text = text;
            playerWonArea.gameObject.SetActive(true);
        }
    }
}