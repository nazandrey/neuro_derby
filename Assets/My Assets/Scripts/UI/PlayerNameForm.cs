using System;
using UnityEngine;
using UnityEngine.UI;

namespace NeuroDerby.UI
{
    public class PlayerNameForm : MonoBehaviour
    {
        [SerializeField]
        private int playerNum;

        [SerializeField]
        private Text playerLabel;

        [SerializeField]
        private InputField playerInput;

        private const string PlayerLabelPrefix = "Player";

        private void Start()
        {
            playerLabel.text = $"{PlayerLabelPrefix} {playerNum + 1}";
        }

        public PlayerNameInputDto GetCurrentValues()
        {
            return new PlayerNameInputDto { Num = playerNum, Name = playerInput.text };
        }
    }
}