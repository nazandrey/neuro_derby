using UnityEngine;

namespace NeuroDerby.UI
{
    public class ShowIfGameplayDebugOnly : MonoBehaviour
    {
        private void Awake()
        {
            #if GAMEPLAY_DEBUG
            gameObject.SetActive(true);
            #else
            gameObject.SetActive(false);
            #endif
        }
    }
}
