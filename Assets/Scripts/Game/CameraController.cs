using Cinemachine;
using UnityEngine;

namespace Game
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 10f;
        private float _rotationY;
        private bool _isDragging;

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _isDragging = true;
            }

            if (Input.GetMouseButtonUp(1))
            {
                _isDragging = false;
            }

            if (!_isDragging)
            {
                return;
            }
            var mouseX = Input.GetAxis("Mouse X");
            _rotationY += mouseX * rotationSpeed;
            transform.rotation = Quaternion.Euler(0, _rotationY, 0);
        }
    }
}