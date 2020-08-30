using UnityEngine;

namespace NeuroDerby.UI
{
    public class MenuChanger : MonoBehaviour
    {
        [SerializeField]
        public GameObject firstMenu;

        [SerializeField]
        public GameObject chooseNamesForm;

        public void Open(MenuForm menuForm)
        {
            switch (menuForm)
            {
                case MenuForm.FirstMenu:
                    firstMenu.SetActive(true);
                    chooseNamesForm.SetActive(false);
                    break;
                case MenuForm.ChooseNameForm:
                    firstMenu.SetActive(false);
                    chooseNamesForm.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }
}