using System;
using Mobik.Common.Core;
using Mobik.Common.Utilities.UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace BlackBall.UI.Views.LevelScene
{
    public class GameplayView : UIView
    {
        public override Type ActualType => typeof(GameplayView);

        [SerializeField] private Button _pauseButton = null!;
        [SerializeField] private BallSpawn _ballSpawn = null!;

        public override void Initialize()
        {
            _pauseButton.onClick.AddListener(PauseGame);
    
            if (_ballSpawn.SpawnedBallController != null)
            {
                _ballSpawn.SpawnedBallController.Died += OnBallDied;
            }
            else
            {
                _ballSpawn.BallSpawned += HandleBallSpawned;
            }
        }

        private void HandleBallSpawned(BallController ballController)
        {
            ballController.Died += OnBallDied;
        }

        private void OnBallDied()
        {
            _viewVisualizer.Visualize<GameOverView, IOptions>(IOptions.NoneOptions);
        }

        private void PauseGame()
        {
            ServiceLocator.ServiceLocatorInstance.PerGameData.Pause.IsPaused = true;
            _viewVisualizer.Visualize<PauseView, IOptions>(IOptions.NoneOptions);
        }
    }
}