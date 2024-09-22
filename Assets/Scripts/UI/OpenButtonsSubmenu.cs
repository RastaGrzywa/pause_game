using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OpenButtonsSubmenu : MonoBehaviour
    {
        [SerializeField] private Image arrowImage;
        [SerializeField] private Sprite arrowDownImage;
        [SerializeField] private Sprite arrowUpImage;

        [SerializeField] private GameObject submenuObject;
        
        private bool _isOpened;

        public void SwitchState()
        {
            if (_isOpened)
            {
                _isOpened = false;
                submenuObject.SetActive(false);
                arrowImage.sprite = arrowDownImage;
            }
            else
            {
                _isOpened = true;
                submenuObject.SetActive(true);
                arrowImage.sprite = arrowUpImage;
            }
        }

        public void CloseSubmenu()
        {
            _isOpened = false;
            submenuObject.SetActive(false);
            arrowImage.sprite = arrowDownImage;
        }
    }
}