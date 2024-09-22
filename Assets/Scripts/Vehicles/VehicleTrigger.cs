using System;
using System.Collections;
using Level;
using POI;
using UnityEngine;

namespace Vehicles
{
    public class VehicleTrigger : MonoBehaviour
    {
        [SerializeField] private Vehicle vehicle;


        private Vehicle _vehicleToCheck;
        private bool _vehicleToCheckWasPaused;
        private float _updateTimer;
        private float _updateTime = 0.1f;

        private void Update()
        {
            if (_vehicleToCheck == null)
            {
                return;
            }

            if (_vehicleToCheck.IsPaused == false && _vehicleToCheckWasPaused == true)
            {
                _vehicleToCheck = null;
                StartCoroutine(DelayedPlay());
                return;
            }

            if (_updateTimer >= _updateTime)
            {
                _updateTimer = 0f;
                _vehicleToCheckWasPaused = _vehicleToCheck.IsPaused;
            }

            _updateTimer += Time.deltaTime;
        }

        private IEnumerator DelayedPlay()
        {
            yield return new WaitForSeconds(0.4f);
            vehicle.Play();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (vehicle.IsPaused)
            {
                return;
            }
            
            if (other.CompareTag("VehicleTrigger"))
            {
                var pause = other.GetComponent<PauseVehicleTrigger>().pause;
                pause.StartPause(vehicle);
            }
            
            if (other.CompareTag("Vehicle"))
            {
                _vehicleToCheck = other.GetComponent<VehicleBodyTrigger>().Vehicle;
                if (vehicle.OnTheSameSpline(_vehicleToCheck))
                {
                    vehicle.Pause();
                }
                else
                {
                    FindObjectOfType<LevelController>().FailSimulation();
                }
            }
            
        }
    }
}