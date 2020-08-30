using UnityEngine;

namespace NeuroDerby
{
    [RequireComponent(typeof(PlayerActionsLogging))]
    [RequireComponent(typeof(GameOverHandler))]
    public class EntryPoint : MonoBehaviour
    {
        private PlayerActionsLogging playerActionsLogger;
        private GameOverHandler gameOverHandler;

        private void Awake()
        {
            playerActionsLogger = GetComponent<PlayerActionsLogging>();
            gameOverHandler = GetComponent<GameOverHandler>();
        }

        private void Start()
        {
            playerActionsLogger.StartLog();
            gameOverHandler.GameOverEvent.EntryPoint = this;
        }

        public void OnGameOver()
        {
            playerActionsLogger.StopLog();
        }
    }
}