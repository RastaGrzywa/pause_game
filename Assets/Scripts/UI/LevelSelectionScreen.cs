using System;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelSelectionScreen : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private GameObject previousLevelButton;
        [SerializeField] private GameObject nextLevelButton;
        [SerializeField] private TextMeshProUGUI playButtonText;
        [SerializeField] private TextMeshProUGUI levelScoreText;


        private Button _previousButton;
        private Button _nextButton;
        private Button _playButton;

        private void Awake()
        {
            _playButton = playButtonText.GetComponent<Button>();
            _previousButton = previousLevelButton.GetComponent<Button>();
            _nextButton = nextLevelButton.GetComponent<Button>();
        }

        public void LockButtons(bool buttonsLocked)
        {
            _previousButton.interactable = !buttonsLocked;
            _nextButton.interactable = !buttonsLocked;
            _playButton.interactable = !buttonsLocked;
        }
        
        public void UpdateVisuals()
        {
            previousLevelButton.SetActive(false);
            nextLevelButton.SetActive(false);
            
            if (gameManager.SelectedLevelId != 0)
            {
                previousLevelButton.SetActive(true);
            }

            if (gameManager.SelectedLevelId < gameManager.LevelsAmount - 1)
            {
                nextLevelButton.SetActive(true);
            }
            
            if (gameManager.SelectedLevelId > gameManager.HighestCompletedLevelId + 1)
            {
                playButtonText.text = "Zablokowane";
                _playButton.interactable = false;
            }
            else
            {
                playButtonText.text = "GRAMY!";
                _playButton.interactable = true;
            }

            if (gameManager.LevelsScore[gameManager.SelectedLevelId] == 0)
            {
                levelScoreText.text = "-";
            }
            else
            {
                levelScoreText.text = gameManager.LevelsScore[gameManager.SelectedLevelId].ToString();
            }
        }
    }
}