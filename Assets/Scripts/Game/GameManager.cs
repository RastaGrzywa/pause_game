using System;
using System.Collections.Generic;
using Level;
using UI;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<LevelController> levels;
        [SerializeField] private List<GameObject> levelsSelection;
        [SerializeField] private LevelSelectionScreen levelSelectionScreen;
        [SerializeField] private ScreensController screensController;
        public int SelectedLevelId;
        public int HighestCompletedLevelId = 1;
        public int LevelsAmount => levels.Count;
        private GameObject _currentLevelObject;

        public int[] LevelsScore = {0, 0, 0, 0, 0};

        private void Start()
        {
            levelSelectionScreen.UpdateVisuals();
        }

        public void SelectPreviousLevel()
        {
            if (SelectedLevelId == 0)
            {
                return;
            }

            levelSelectionScreen.LockButtons(true);
            LeanTween.scale(levelsSelection[SelectedLevelId], Vector3.zero, 0.4f).setEase(LeanTweenType.easeInBack)
                .setOnComplete(() =>
                {
                    SelectedLevelId--;
                    ShowSelectedLevel();
                });
            levelSelectionScreen.UpdateVisuals();
        }

        public void SelectNextLevel()
        {
            if (SelectedLevelId >= LevelsAmount - 1)
            {
                return;
            }

            levelSelectionScreen.LockButtons(true);

            LeanTween.scale(levelsSelection[SelectedLevelId], Vector3.zero, 0.4f).setEase(LeanTweenType.easeInBack)
                .setOnComplete(() =>
                {
                    SelectedLevelId++;
                    ShowSelectedLevel();
                });
            levelSelectionScreen.UpdateVisuals();
        }

        public void PlaySelectedLevel()
        {
            _currentLevelObject = Instantiate(levels[SelectedLevelId]).gameObject;
            levelsSelection[SelectedLevelId].SetActive(false);
            screensController.HideSelectionScreen();
            screensController.ShowGameplayScreen();
        }

        public void NextLevelClicked()
        {
            DestroyCurrentLevel();
            SelectedLevelId++;
            PlaySelectedLevel();
        }

        public void DestroyCurrentLevel()
        {
            DestroyImmediate(_currentLevelObject);
        }

        public void ShowSelectedLevel()
        {
            levelsSelection[SelectedLevelId].SetActive(true);
            LeanTween.scale(levelsSelection[SelectedLevelId], Vector3.one, 0.4f).setEase(LeanTweenType.easeOutBack)
                .setOnComplete(() =>
                {
                    levelSelectionScreen.LockButtons(false);
                    levelSelectionScreen.UpdateVisuals();
                });
        }

        public void HideSelectedLevel()
        {
            LeanTween.scale(levelsSelection[SelectedLevelId], Vector3.zero, 0.4f).setEase(LeanTweenType.easeInBack)
                .setOnComplete(
                    () => { levelsSelection[SelectedLevelId].SetActive(false); });
        }
    }
}