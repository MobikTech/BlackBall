using System;
using UnityEngine;

namespace BlackBall.Services.PerGameServices
{
    public class Pause : IResetableService
    {
        public event Action? Paused;
        public event Action? Unpaused;
        private bool _isPaused;
        

        public bool IsPaused
        {
            get => _isPaused;
            set
            {
                if (value == _isPaused) return;
                if (value) PauseGame();
                else ResumeGame();
                _isPaused = value;
            }
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
            Paused?.Invoke();
        }
        
        private void ResumeGame()
        {
            Time.timeScale = 1;
            Unpaused?.Invoke();
        }

        public void Reset()
        {
            Time.timeScale = 1;
            _isPaused = false;
        }
    }
}