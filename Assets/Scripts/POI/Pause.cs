using System;
using System.Collections;
using Level;
using Player;
using TMPro;
using UnityEngine;
using Vehicles;

namespace POI
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] private GameObject defaultIndicatorGameObject;
        [SerializeField] private GameObject hoverIndicatorGameObject;

        [SerializeField] private Transform indicatorSpawnTransform;
        [SerializeField] private PauseIndicator singlePauseIndicator;
        [SerializeField] private PauseIndicator multiplePauseIndicator;

        [SerializeField] private GameObject hitText;
        public bool IsActive = false;
        public bool IsTutorial = false;


        private PlayerController _playerController;
        private int _currentPauseTime;
        private bool _isMultiplePause;
        private bool _isSimulationPaused;
        private bool _isSimulationFinished;
        private PauseIndicator _pauseIndicator;
        private LevelController _levelController;

        private void Awake()
        {
            _playerController = FindObjectOfType<PlayerController>();
            if (IsTutorial)
            {
                hitText.SetActive(true);
            }
            else
            {
                hitText.SetActive(false);
            }
        }

        private void OnEnable()
        {
            _levelController = FindObjectOfType<LevelController>();
            _levelController.SubscribePause(this);
        }

        public void OnSimulationReset()
        {
            _isSimulationPaused = false;
            _isSimulationFinished = true;
            Invoke(nameof(DelayedBreakReset), 0.7f);
        }

        private void DelayedBreakReset()
        {
            _isSimulationFinished = false;
        }
        
        public void OnSimulationContinued()
        {
            _isSimulationPaused = false;
        }
        
        public void OnSimulationPaused()
        {
            _isSimulationPaused = true;
        }

        public void OnSimulationFinished()
        {
            _isSimulationFinished = true;
        }

        public void OnElementClicked()
        {
            if (_playerController.SelectedPauseTime == 0)
            {
                Deactivate();
            }
            else
            {
                Activate();
            }
        }

        private void Activate()
        {
            if (_pauseIndicator != null)
            {
                DestroyImmediate(_pauseIndicator.gameObject);
                _levelController.UpdateUsedSeconds(_currentPauseTime * -1);
            }
            
            if (IsTutorial)
            {
                hitText.SetActive(false);
            }

            IsActive = true;
            var secondsLeft = _levelController.LevelData.AvailableSeconds - _levelController.LevelData.UsedSeconds;
            if (_playerController.SelectedPauseTime > secondsLeft)
            {
                _playerController.ChangePauseTimeTo(secondsLeft);
            }
            if (_playerController.SelectedPauseTime == 0)
            {
                Deactivate();
            }
            _isMultiplePause = _playerController.IsMultiplePause;
            _currentPauseTime = _playerController.SelectedPauseTime;
            _levelController.UpdateUsedSeconds(_currentPauseTime);

            if (_isMultiplePause)
            {
                _pauseIndicator = Instantiate(multiplePauseIndicator, indicatorSpawnTransform);
            }
            else
            {
                _pauseIndicator = Instantiate(singlePauseIndicator, indicatorSpawnTransform);
            }

            hoverIndicatorGameObject.SetActive(false);
            defaultIndicatorGameObject.SetActive(false);

            UpdateTimeText(_currentPauseTime);
        }

        private void Deactivate()
        {
            if (_pauseIndicator != null)
            {
                DestroyImmediate(_pauseIndicator.gameObject);     
                _levelController.UpdateUsedSeconds(_currentPauseTime * -1);
                _currentPauseTime = 0;
            }

            hoverIndicatorGameObject.SetActive(false);
            defaultIndicatorGameObject.SetActive(true);
            IsActive = false;
        }

        public void EnableHoverIndicator(bool enabled)
        {
            if (_pauseIndicator != null)
            {
                _pauseIndicator.UpdateHoverIndicator(enabled);
            }
            else
            {
                hoverIndicatorGameObject.SetActive(enabled);
            }
        }


        public void StartPause(Vehicle vehicleToPause)
        {
            if (!IsActive)
            {
                return;
            }

            StartCoroutine(PauseVehicle(vehicleToPause));
        }

        private IEnumerator PauseVehicle(Vehicle vehicleToPause)
        {
            vehicleToPause.PauseByPause();
            for (var i = _currentPauseTime - 1; i >= 0; i--)
            {
                yield return new WaitForSeconds(1f);
                if (_isSimulationFinished)
                {
                    UpdateTimeText(_currentPauseTime);
                    yield break;
                }
                yield return new WaitUntil(() => _isSimulationPaused == false);
                UpdateTimeText(i);
            }
            if (_isSimulationFinished)
            {
                UpdateTimeText(_currentPauseTime);
                yield break;
            }
            yield return new WaitUntil(() => _isSimulationPaused == false);
            vehicleToPause.PlayByPause();
            if (_isMultiplePause == false)
            {
                Deactivate();
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
                UpdateTimeText(_currentPauseTime);
            }
        }

        private void UpdateTimeText(int newTime)
        {
            if (_pauseIndicator != null)
            {
                _pauseIndicator.UpdateText(newTime);
            }
        }
    }
}