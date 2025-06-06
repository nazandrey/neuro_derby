﻿using NeuroDerby.Game.EventDatas;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace NeuroDerby.Game
{
    public class GameOverHandler : MonoBehaviour
    {
        public Health player1Health;
        public Health player2Health;
        public Text playerWonText;
        public GameObject playerWonArea;

        public GameOverEvent GameOverEvent { get; private set; }

        private GameState _gameState;

        [Inject]
        public void Construct(GameOverEvent.Factory gameOverEventFactory, GameState gameState)
        {
            GameOverEvent = gameOverEventFactory.Create();
            _gameState = gameState;
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
                HandleGameOver(false, 0, 1);
            else if (player1Health.IsDead)
                HandleGameOver(false, 1, 0);
        }

        private void HandleGameOver(bool isDraw, int winnerNum = 0, int loserNum = default)
        {
            var winnerName = _gameState.GetPlayerNameByNum(winnerNum);
            var gameOverText = isDraw ? "DRAW!" : $"{winnerName} (player {winnerNum + 1}) WON!";
            ShowPlayerGameOverText(gameOverText);
            GameOverEvent.Dispatch(new GameOverEventData(isDraw, winnerNum, loserNum));
            _gameState.SetGameIsOver(true);
        }

        private void ShowPlayerGameOverText(string text)
        {
            playerWonText.text = text;
            playerWonArea.gameObject.SetActive(true);
        }
    }
}