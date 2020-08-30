using UnityEngine.SceneManagement;

namespace NeuroDerby.Core
{
    public static class SceneHelpers
    {
        public static void LoadScene(SceneName sceneName)
        {
            SceneManager.LoadScene(sceneName.ToString());
        }
    }
}