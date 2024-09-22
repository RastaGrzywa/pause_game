using System;
using System.Collections.Generic;
using System.Linq;
using POI;
using UI;
using UnityEngine;
using UnityEngine.Splines;
using Vehicles;

namespace Level
{
    public class LevelController : MonoBehaviour
    {
        public int CompletionAmount;
        public int AvailableSeconds;
        public bool IsTutorial;

        private HashSet<Vehicle> _vehicles;
        private HashSet<Pause> _pauses = new HashSet<Pause>();

        private bool _isPaused;
        private bool _isPlaying;
        private ScreensController _screensController;

        private float _simulationTimer;

        private LevelData _levelData;
        public LevelData LevelData => _levelData;

        private TopMainPanel _topMainPanel;

        public void SubscribeVehicle(Vehicle vehicle)
        {
            if (_vehicles == null)
            {
                _vehicles = new HashSet<Vehicle>();
            }

            _vehicles.Add(vehicle);
        }

        public void SubscribePause(Pause pause)
        {
            if (_pauses == null)
            {
                _pauses = new HashSet<Pause>();
            }

            _pauses.Add(pause);
        }

        public void VehicleCompletedSimulation()
        {
            foreach (var vehicle in _vehicles)
            {
                if (!vehicle.gameObject.activeInHierarchy)
                {
                    continue;
                }
                if (vehicle.ContinousMove == false)
                {
                    return;
                }
            }

            PauseSimulation();
            _levelData.LevelCompleted = true;
            _screensController.ShowEndgameScreen();
            CompletedSimulation();
        }

        private void OnEnable()
        {
            FindObjectOfType<SimulationButtons>().SubscribeToButtons(this);
            _screensController = FindObjectOfType<ScreensController>();
            _levelData = new LevelData();
            _levelData.AvailableSeconds = AvailableSeconds;
            _topMainPanel = FindObjectOfType<TopMainPanel>();
            UpdateDataDisplay();
        }

        private void Update()
        {
            if (_isPlaying == false)
            {
                return;
            }

            _simulationTimer += Time.deltaTime;
            if (_simulationTimer >= 1f)
            {
                _levelData.SimulationTime++;
                _simulationTimer = 0f;
                UpdateDataDisplay();
            }
        }

        private void UpdateDataDisplay()
        {
            _topMainPanel.UpdateVisuals(_levelData);
        }

        public void PlaySimulation()
        {
            if (_isPaused)
            {
                foreach (var vehicle in _vehicles)
                {
                    vehicle.Play();
                }
                
                foreach (var pause in _pauses)
                {
                    pause.OnSimulationContinued();
                }
            }
            else
            {
                foreach (var vehicle in _vehicles)
                {
                    vehicle.StartSimulation();
                }
                foreach (var pause in _pauses)
                {
                    pause.OnSimulationReset();
                }
            }

            _isPlaying = true;
        }

        public void PauseSimulation()
        {
            _isPaused = true;
            _isPlaying = false;
            foreach (var vehicle in _vehicles)
            {
                vehicle.Pause();
            }

            foreach (var pause in _pauses)
            {
                pause.OnSimulationPaused();
            }
        }

        public void ResetSimulation()
        {
            _isPaused = false;
            _isPlaying = false;
            _levelData.SimulationTime = 0;
            UpdateDataDisplay();
            foreach (var vehicle in _vehicles)
            {
                vehicle.Restart();
            }
            foreach (var pause in _pauses)
            {
                pause.OnSimulationReset();
            }
        }

        public void FailSimulation()
        {
            _levelData.LevelCompleted = false;
            _screensController.ShowEndgameScreen();
            foreach (var pause in _pauses)
            {
                pause.OnSimulationFinished();
            }
            PauseSimulation();
        }

        public void CompletedSimulation()
        {
            foreach (var pause in _pauses)
            {
                pause.OnSimulationFinished();
            }
        }

        public void UpdateUsedSeconds(int secondsToAdd)
        {
            _levelData.UsedSeconds += secondsToAdd;
            UpdateDataDisplay();
        }
    }

    public class LevelData
    {
        public bool LevelCompleted;
        public int UsedSeconds;
        public int AvailableSeconds;
        public int SimulationTime;
        public int SimulationCost;
        public int LevelScore;
    }
}