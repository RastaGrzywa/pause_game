using System;
using Audio;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private TextMeshProUGUI _buttonText;
        
        private void Awake()
        {
            _buttonText = GetComponent<TextMeshProUGUI>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _buttonText.fontStyle = FontStyles.Underline;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _buttonText.fontStyle = FontStyles.Normal;
        }

        private void OnDisable()
        {
            _buttonText.fontStyle = FontStyles.Normal;
        }
    }
}