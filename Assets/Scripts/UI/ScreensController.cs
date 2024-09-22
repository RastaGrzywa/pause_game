using System;
using Game;
using UnityEngine;

namespace UI
{
    public class ScreensController : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private GameObject endGameObject;
        [SerializeField] private GameObject menuScreenObject;
        [SerializeField] private RectTransform menuMainButtonsObject;
        [SerializeField] private RectTransform menuLevelSelectionObject;
        [SerializeField] private RectTransform gamplayScreenObject;
        [SerializeField] private RectTransform controlScreenObject;
        [SerializeField] private RectTransform creditsScreenObject;

        private CanvasGroup mainMenuButtonsCanvasGroup;

        private void Awake()
        {
            mainMenuButtonsCanvasGroup = menuMainButtonsObject.GetComponent<CanvasGroup>();
        }

        public void ShowEndgameScreen()
        {
            endGameObject.SetActive(true);
        }

        public void HideEndgameScreen()
        {
            endGameObject.SetActive(false);
        }

        public void ShowMainMenuButtons()
        {
            menuMainButtonsObject.gameObject.SetActive(true);
            LeanTween.alphaCanvas(mainMenuButtonsCanvasGroup, 1f, 0.35f);
        }

        public void HideMainMenuButtons()
        {
            LeanTween.alphaCanvas(mainMenuButtonsCanvasGroup, 0f, 0.35f).setOnComplete(() =>
            {
                menuMainButtonsObject.gameObject.SetActive(false);
            });
        }
        
        public void ShowSelectionScreen()
        {
            LeanTween.moveY(menuLevelSelectionObject, 100f, 0.4f).setEase(LeanTweenType.easeInOutBack);
            gameManager.ShowSelectedLevel();
        }
        public void HideSelectionScreen()
        {
            LeanTween.moveY(menuLevelSelectionObject, -100f, 0.4f).setEase(LeanTweenType.easeInOutBack);
            gameManager.HideSelectedLevel();
        }

        public void ShowGameplayScreen()
        {
            LeanTween.moveY(gamplayScreenObject, -50f, 0.4f).setEase(LeanTweenType.easeInOutBack);
        }
        
        public void HideGameplayScreen()
        {
            LeanTween.moveY(gamplayScreenObject, 50f, 0.4f).setEase(LeanTweenType.easeInOutBack);
        }

        public void ShowControlScreen()
        {
            LeanTween.scale(controlScreenObject, Vector3.one, 0.4f).setEase(LeanTweenType.easeOutBack);
        }
        
        public void HideControlScreen()
        {
            LeanTween.scale(controlScreenObject, Vector3.zero, 0.4f).setEase(LeanTweenType.easeInBack);
        }
        
        public void ShowCreditsScreen()
        {
            LeanTween.scale(creditsScreenObject, Vector3.one, 0.4f).setEase(LeanTweenType.easeOutBack);
        }
        
        public void HideCreditsScreen()
        {
            LeanTween.scale(creditsScreenObject, Vector3.zero, 0.4f).setEase(LeanTweenType.easeInBack);
        }
    }
}