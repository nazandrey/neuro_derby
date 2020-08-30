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
            restartButton?.onClick.AddListener(() => SceneManager.LoadScene(0));
        }
    }
}