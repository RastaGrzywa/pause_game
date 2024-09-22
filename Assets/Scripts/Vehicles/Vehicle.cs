using System;
using System.Collections;
using Level;
using POI;
using UnityEngine;
using UnityEngine.Splines;

namespace Vehicles
{
    public class Vehicle : MonoBehaviour
    {
        [SerializeField] private float delayTimeOnStart;
        [SerializeField] private bool continousMove;
        private SplineAnimate _splineAnimate;
        public SplineAnimate SplineAnimate => _splineAnimate;

        public bool IsPaused => !_splineAnimate.IsPlaying;
        private bool _isPausedByPause;
        
        private int _completedAmount;
        private LevelController _levelController;
        public bool ContinousMove => continousMove;

        private void Start()
        {
            _splineAnimate = GetComponent<SplineAnimate>();
            _levelController = FindObjectOfType<LevelController>();
            _levelController.SubscribeVehicle(this);
            _splineAnimate.Completed += SplineAnimateOnCompleted;
        }

        private void SplineAnimateOnCompleted()
        {
            if (continousMove)
            {
                return;
            }
            _completedAmount++;
            if (_completedAmount >= _levelController.CompletionAmount)
            {
                gameObject.SetActive(false);
                _levelController.VehicleCompletedSimulation();
            }
        }

        public void Pause()
        {
            _splineAnimate.Pause();
        }
        
        public void PauseByPause()
        {
            _isPausedByPause = true;
            Pause();
        }


        public void Play()
        {
            if (_isPausedByPause)
            {
                return;
            }
            _splineAnimate.Play();
        }

        public void PlayByPause()
        {
            _isPausedByPause = false;
            Play();
        }
        public void Restart()
        {
            gameObject.SetActive(true);
            _splineAnimate.Restart(false);
            _completedAmount = 0;
            _isPausedByPause = false;
        }

        public void StartSimulation()
        {
            StartCoroutine(DelayedPlay());
        }

        private IEnumerator DelayedPlay()
        {
            yield return new WaitForSeconds(delayTimeOnStart);
            Play();
        }

        public bool OnTheSameSpline(Vehicle vehicleToCheck) =>
            _splineAnimate.Container == vehicleToCheck.SplineAnimate.Container;
    }
}