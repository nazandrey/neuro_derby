using UnityEngine;

namespace NeuroDerby.UI
{
    public class ChoosingNameForm : MonoBehaviour
    {
        [SerializeField]
        private GameObject sameNameErrorTooltip;

        public void SetSameNameErrorActive(bool isActive)
            => sameNameErrorTooltip.SetActive(isActive);
    }
}