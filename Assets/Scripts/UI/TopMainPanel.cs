using System;
using Level;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TopMainPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI simulationTimeText;
        [SerializeField] private TextMeshProUGUI pauseSecondsText;

        [SerializeField] private SubmenuController singleTimesSubmenu;
        [SerializeField] private SubmenuController multipleTimesSubmenu;
        
        public void UpdateVisuals(LevelData levelData)
        {
            var time = TimeSpan.FromSeconds(levelData.SimulationTime);
            simulationTimeText.text = $"{time.Minutes:00}:{time.Seconds:00}";
            var secondsLeft = levelData.AvailableSeconds - levelData.UsedSeconds;
            pauseSecondsText.text = $"{secondsLeft}";
            singleTimesSubmenu.EnableSeconds(secondsLeft);
            multipleTimesSubmenu.EnableSeconds(secondsLeft);
        }
    }
}