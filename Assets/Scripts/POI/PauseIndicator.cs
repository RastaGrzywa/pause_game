using TMPro;
using UnityEngine;

namespace POI
{
    public class PauseIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshPro timeText;
        [SerializeField] private GameObject hoverIndicatorObject;
        
        public void UpdateText(int time)
        {
            timeText.text = $"{time}";
        }

        public void UpdateHoverIndicator(bool isEnabled)
        {
            hoverIndicatorObject.SetActive(isEnabled);
        }
    }
}