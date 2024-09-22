using Level;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SimulationButtons : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button playButton;
        [SerializeField] private Image timeHintObject;
        
        public void SubscribeToButtons(LevelController levelController)
        {
            restartButton.onClick.RemoveAllListeners();
            pauseButton.onClick.RemoveAllListeners();
            playButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(levelController.ResetSimulation);
            pauseButton.onClick.AddListener(levelController.PauseSimulation);
            playButton.onClick.AddListener(levelController.PlaySimulation);
            if (levelController.IsTutorial)
            {
                timeHintObject.enabled = true;
            }
            else
            {
                timeHintObject.enabled = false;
            }
        }
    }
}