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
                HandleGameOver(true);
            else if (player2Health.IsDead)
                HandleGameOver(false, 0);
            else if (player1Health.IsDead)
                HandleGameOver(false, 1);
        }

        private void HandleGameOver(bool isDraw, int winnerNum = 0)
        {
            var gameOverText = isDraw ? "DRAW!" : $"Player {winnerNum + 1} WON!";
            ShowPlayerGameOverText(gameOverText);
            GameOverEvent.Dispatch(new GameOverEventData(winnerNum));
        }

        private void ShowPlayerGameOverText(string text)
        {
            playerWonText.text = text;
            playerWonArea.gameObject.SetActive(true);
        }
    }
}