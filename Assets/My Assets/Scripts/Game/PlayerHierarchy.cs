using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace NeuroDerby.Game
{
    public class PlayerHierarchy : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private PlayerInputController inputController;
        [SerializeField] private int playerNum;
        [SerializeField] private Text playerName;

        public Health Health => health;
        public PlayerInputController InputController => inputController;
        public int PlayerNum => playerNum;
        
        private GameState _gameState;

        [Inject]
        private void Construct(GameState gameState)
        {
            _gameState = gameState;
        }

        private void Awake()
        {
            if (playerNum > 1)
                throw new System.Exception("More than two players is not supported");
            playerName.text = _gameState.GetPlayerNameByNum(playerNum);
        }
    }
}