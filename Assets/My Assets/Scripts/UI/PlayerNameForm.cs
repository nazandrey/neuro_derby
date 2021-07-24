using NeuroDerby.Players;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace NeuroDerby.UI
{
    public class PlayerNameForm : MonoBehaviour
    {
        private const string PlayerLabelPrefix = "Player";

        [SerializeField]
        private int playerNum;

        [SerializeField]
        private Text playerLabel;

        [SerializeField]
        private InputField playerInput;
        
        [SerializeField]
        private Image playerBg;
        
        [SerializeField]
        private GameObject validationErrorToolip;
        
        [SerializeField]
        private Text validationErrorToolipText;

        private IPlayerNameChecker _playerNameChecker;
        
        public bool IsValid { get; private set; }

        [Inject]
        public void Construct(IPlayerNameChecker playerNameChecker)
        {
            _playerNameChecker = playerNameChecker;
        }
        
        private void Awake()
        {
            validationErrorToolipText.text = _playerNameChecker.GetTooltipTextForInvalidName();
            playerInput.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(string inputName)
        {
            IsValid = _playerNameChecker.Check(inputName, out var checkedPlayerName)
                && IsTrimmedInputTextValid(inputName, checkedPlayerName);
            
            playerBg.color = IsValid ? Color.green : Color.red;
            validationErrorToolip.SetActive(!IsValid);
        }

        private static bool IsTrimmedInputTextValid(string inputName, string checkedPlayerName)
        {
            return inputName.Trim() == checkedPlayerName;
        }

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