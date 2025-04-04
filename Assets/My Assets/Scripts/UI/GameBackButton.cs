using NeuroDerby.Core;
using UnityEngine;
using UnityEngine.UI;

namespace NeuroDerby.UI
{
    public class GameBackButton : BackButton
    {
        protected override void OnBackButtonClick()
        {
            SceneHelpers.LoadScene(SceneName.Menu);
        }
    }
}