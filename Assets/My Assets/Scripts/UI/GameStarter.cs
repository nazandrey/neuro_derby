using System.Collections.Generic;
using System.Linq;
using NeuroDerby.Core;
using NeuroDerby.Extensions;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace NeuroDerby.UI
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField]
        private List<PlayerNameForm> playerNameForms;

        [SerializeField]
        private Button goButton;
        
        [SerializeField]
        private ChoosingNameForm choosingNameForm;

        private bool _hasSameNames;
        private GameState _gameState;
        
        [Inject]
        private void Construct(GameState gameState)
        {
            _gameState = gameState;
        }

        private void Awake()
        {
            goButton.onClick.AddListener(StartGame);
            foreach (var playerNameForm in playerNameForms) 
                playerNameForm.AddOnValueChangedListener(CheckHasSameNames);
        }

        private void OnDestroy()
        {
            goButton.onClick.RemoveListener(StartGame);
            foreach (var playerNameForm in playerNameForms)
                playerNameForm.RemoveOnValueChangedListener(CheckHasSameNames);
        }

        private void CheckHasSameNames(string _)
        {
            var playerNames 
                = playerNameForms.Select(x => x.GetCurrentValues().Name);
            _hasSameNames = playerNames.HasDuplicates();
            choosingNameForm.SetSameNameErrorActive(_hasSameNames);
        }

        private void StartGame()
        {
            if (!playerNameForms.All(x => x.IsValid))
                return;

            if (_hasSameNames)
                return;

            _gameState.SetGameIsOver(false);
            _gameState.ClearPlayerNames();
            foreach (var playerNameForm in playerNameForms)
            {
                var playerNameInputDto = playerNameForm.GetCurrentValues();
                _gameState.AddPlayer(playerNameInputDto.Num, playerNameInputDto.Name);
            }

            SceneHelpers.LoadScene(SceneName.Game);
        }
    }
}