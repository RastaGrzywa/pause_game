using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class SubmenuController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> timeButtons;

        public void EnableSeconds(int seconds)
        {
            foreach (var timeButton in timeButtons)
            {
                timeButton.SetActive(timeButtons.IndexOf(timeButton) <= seconds);
            }
        }
    }
}