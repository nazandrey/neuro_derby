using UnityEngine;

namespace NeuroDerby
{
    public class PlayerHierarchy : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private PlayerInputController inputController;
        [SerializeField] private int playerNum;

        public Health Health => health;
        public PlayerInputController InputController => inputController;
        public int PlayerNum => playerNum;

        private void Awake()
        {
            if (playerNum > 1)
                throw new System.Exception("More than two players is not supported");
        }
    }
}