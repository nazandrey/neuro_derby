using System;
using UnityEngine.SceneManagement;

namespace NeuroDerby.Core
{
    public static class SceneHelpers    
    {
        private static int previousSceneIndex;
        
        static SceneHelpers()
        {
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private static void OnSceneUnloaded(Scene scene)
        {
            previousSceneIndex = scene.buildIndex;
        }

        public static void LoadScene(SceneName sceneName)
        {
            SceneManager.LoadScene(sceneName.ToString());
        }
        
        public static void LoadPreviousScene()
        {
            SceneManager.LoadScene(previousSceneIndex);
        }
    }
}