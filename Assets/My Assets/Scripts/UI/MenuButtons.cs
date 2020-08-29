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

        private void OnStartButtonClick()
        {
            SceneManager.LoadScene(SceneName.Game.ToString());
        }

        private void OnScoreTableButtonClick()
        {
            SceneManager.LoadScene(SceneName.ScoreTable.ToString());
        }
    }
}