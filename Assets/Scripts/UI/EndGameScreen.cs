using System;
using Audio;
using Game;
using Level;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EndGameScreen : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private AudioSystem audioSystem;
        [SerializeField] private TextMeshProUGUI simulationTimeText;
        [SerializeField] private TextMeshProUGUI usedSecondsText;
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private TextMeshProUGUI finalScoreText;

        [SerializeField] private Button nextMapButton;
        [SerializeField] private GameObject menuButton;
        [SerializeField] private GameObject defaultMenuButton;
        

        private void OnEnable()
        {
            var levelController = FindObjectOfType<LevelController>();
            nextMapButton.interactable = levelController.LevelData.LevelCompleted;
            menuButton.SetActive(false);
            defaultMenuButton.SetActive(true);

            
            var time = TimeSpan.FromSeconds(levelController.LevelData.SimulationTime);
            simulationTimeText.text = $"{time.Minutes:00}:{time.Seconds:00}";

            usedSecondsText.text = levelController.LevelData.UsedSeconds.ToString();
            costText.text = "-";

            var points = 1000;
            points -= levelController.LevelData.SimulationTime * 10;
            points -= levelController.LevelData.UsedSeconds * 50;
            finalScoreText.text = points.ToString();
            
            if (levelController.LevelData.LevelCompleted)
            {
                gameManager.LevelsScore[gameManager.SelectedLevelId] = points;
                gameManager.HighestCompletedLevelId = gameManager.SelectedLevelId;
                
                if (gameManager.SelectedLevelId == gameManager.LevelsAmount - 1)
                {
                    menuButton.SetActive(true);
                    defaultMenuButton.SetActive(false);
                }
                audioSystem.PlayLevelCompleted();
            }
            else
            {
                audioSystem.PlayLevelFailed();
            }
        }

        public void RestartLevel()
        {
            FindObjectOfType<LevelController>().ResetSimulation();
        }
    }
}