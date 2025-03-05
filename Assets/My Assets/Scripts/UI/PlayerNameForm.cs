using NeuroDerby.Players;
using NeuroDerby.RatingSystem;
using NeuroDerby.RatingSystem.Glicko;
using UnityEngine;
using UnityEngine.Events;
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
        private IPlayerNameCleaner _playerNameCleaner;

        public bool IsValid { get; private set; }

        [Inject]
        public void Construct(IPlayerNameChecker playerNameChecker, 
            IScoreStorage<string, Player> scoreStorage,
            IPlayerNameCleaner playerNameCleaner)
        {
            _playerNameChecker = playerNameChecker;
            _scoreStorage = scoreStorage;
            _playerNameCleaner = playerNameCleaner;
        }
        
        private void Awake()
        {
            validationErrorToolipText.text = _playerNameChecker.GetTooltipTextForInvalidName();
            playerInput.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDestroy()
        {
            playerInput.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(string inputName)
        {
            IsValid = _playerNameChecker.Check(inputName);
            
            playerBg.color = IsValid ? colorForValid : colorForInavlid;
            validationErrorToolip.SetActive(!IsValid);

            var clearedPlayerName = _playerNameCleaner.Clean(inputName);
            var playerNameExists = _scoreStorage.TryGetScore(clearedPlayerName, out _);
            playerNameExistsWarning.SetActive(playerNameExists);
        }

        private void Start()
        {
            playerLabel.text = $"{PlayerLabelPrefix} {playerNum + 1}";
        }

        public PlayerNameInputDto GetCurrentValues()
        {
            return new() { Num = playerNum, Name = _playerNameCleaner.Clean(playerInput.text) };
        }

        public void AddOnValueChangedListener(UnityAction<string> action) 
            => playerInput.onValueChanged.AddListener(action);
        
        public void RemoveOnValueChangedListener(UnityAction<string> action) 
            => playerInput.onValueChanged.RemoveListener(action);
    }
}