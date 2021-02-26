using NeuroDerby.Core;
using UnityEngine;
using UnityEngine.UI;

namespace NeuroDerby.UI
{
    public class MenuButtons : MonoBehaviour
    {
        [SerializeField]
        private Button startButton;

        [SerializeField]
        private Button scoreTableButton;

        [SerializeField]
        private MenuChanger menuChanger;

        private void Awake()
        {
            startButton.onClick.AddListener(OnStartButtonClick);
            scoreTableButton.onClick.AddListener(OnScoreTableButtonClick);
        }

        private void OnDestroy()
        {
            startButton.onClick.RemoveListener(OnStartButtonClick);
            scoreTableButton.onClick.RemoveListener(OnScoreTableButtonClick);
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