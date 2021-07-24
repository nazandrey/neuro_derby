using NeuroDerby.Players;
using NeuroDerby.RatingSystem;
using NeuroDerby.RatingSystem.Glicko;
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
        
        [SerializeField]
        private GameObject playerNameExistsWarning;
        
        [SerializeField]
        private Color colorForValid;
        
        [SerializeField]
        private Color colorForInavlid;

        private IPlayerNameChecker _playerNameChecker;
        private IScoreStorage<string, Player> _scoreStorage;

        public bool IsValid { get; private set; }

        [Inject]
        public void Construct(IPlayerNameChecker playerNameChecker, IScoreStorage<string, Player> scoreStorage)
        {
            _playerNameChecker = playerNameChecker;
            _scoreStorage = scoreStorage;
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
            
            playerBg.color = IsValid ? colorForValid : colorForInavlid;
            validationErrorToolip.SetActive(!IsValid);

            var playerNameExits = _scoreStorage.TryGetScore(checkedPlayerName, out _);
            playerNameExistsWarning.SetActive(playerNameExits);
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