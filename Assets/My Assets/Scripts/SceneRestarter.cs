using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NeuroDerby
{
    public class SceneRestarter : MonoBehaviour
    {
        [SerializeField]
        private Button restartButton;

        private void Start()
        {
            GameState.SetGameIsOver(false);
            restartButton?.onClick.AddListener(() => SceneManager.LoadScene("Game"));
        }
    }
}