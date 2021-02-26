using NeuroDerby.Core;
using UnityEngine;
using UnityEngine.UI;

namespace NeuroDerby.UI
{
    public class BackButton : MonoBehaviour
    {
        [SerializeField] 
        private Button backButton;
        
        private void Awake()
        {
            backButton.onClick.AddListener(OnBackButtonClick);
        }

        private void OnDestroy()
        {
            backButton.onClick.RemoveListener(OnBackButtonClick);
        }

        private void OnBackButtonClick()
        {
            SceneHelpers.LoadPreviousScene();
        }
    }
}