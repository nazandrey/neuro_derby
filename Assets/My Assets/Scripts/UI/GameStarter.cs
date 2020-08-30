using System.Collections.Generic;
using NeuroDerby.Core;
using UnityEngine;
using UnityEngine.UI;

namespace NeuroDerby.UI
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField]
        private List<PlayerNameForm> playerNameForms;

        [SerializeField]
        private Button goButton;


        private void Awake()
        {
            goButton.onClick.AddListener(StartGame);
        }

        private void OnDestroy()
        {
            goButton.onClick.RemoveListener(StartGame);
        }

        private void StartGame()
        {
            GameState.ClearPlayerNames();
            foreach (var playerNameForm in playerNameForms)
            {
                var playerNameInputDto = playerNameForm.GetCurrentValues();
                GameState.AddPlayer(playerNameInputDto.Num, playerNameInputDto.Name);
            }

            SceneHelpers.LoadScene(SceneName.Game);
        }
    }
}