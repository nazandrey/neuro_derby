using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Scripts.UI
{
    public class MenuButtons : MonoBehaviour
    {
        [SerializeField]
        private Button StartButton;

        [SerializeField]
        private Button ScoreTableButton;

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
            LoadScene(SceneName.Game);
        }

        private void OnScoreTableButtonClick()
        {
            LoadScene(SceneName.ScoreTable);
        }

        private void LoadScene(SceneName sceneName)
        {
            SceneManager.LoadScene(sceneName.ToString());
        }
    }
}