using NeuroDerby.Core;
using UnityEngine;
using UnityEngine.UI;

namespace NeuroDerby.UI
{
    public class MenuButtons : MonoBehaviour
    {
        [SerializeField]
        private Button StartButton;

        [SerializeField]
        private Button ScoreTableButton;

        [SerializeField]
        private MenuChanger menuChanger;

        private void Awake()
        {
            StartButton.onClick.AddListener(OnStartButtonClick);
            ScoreTableButton.onClick.AddListener(OnScoreTableButtonClick);
        }

        private void OnDestroy()
        {
            StartButton.onClick.RemoveListener(OnStartButtonClick);
            ScoreTableButton.onClick.RemoveListener(OnScoreTableButtonClick);
        }

        private void OnStartButtonClick()
        {
            menuChanger.Open(MenuForm.ChooseNameForm);
        }

        private void OnScoreTableButtonClick()
        {
            SceneHelpers.LoadScene(SceneName.ScoreTable);
        }
    }
}