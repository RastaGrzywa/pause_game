using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private int _currentSelectedPauseTime;
        private bool _isMultiplePause;

        public int SelectedPauseTime => _currentSelectedPauseTime;
        public bool IsMultiplePause => _isMultiplePause;

        public void SelectPauseTime(int pauseTime, bool isMultiplePause)
        {
            _currentSelectedPauseTime = pauseTime;
            _isMultiplePause = isMultiplePause;
        }

        public void ChangePauseTimeTo(int pauseTime)
        {
            _currentSelectedPauseTime = pauseTime;
        }
    }
}