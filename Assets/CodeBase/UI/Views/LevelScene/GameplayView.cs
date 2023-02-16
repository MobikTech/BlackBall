using System;
using BlackBall.Core;
using BlackBall.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace BlackBall.UI.Views.LevelScene
{
    public class GameplayView : UIView
    {
        public override Type ActualType => typeof(GameplayView);

        [SerializeField] private Button _pauseButton = null!;
        [SerializeField] private BallController _ballController = null!;

        internal override void Initialize(IViewVisualizer viewVisualizer)
        {
            base.Initialize(viewVisualizer);
            _pauseButton.onClick.AddListener(PauseGame);
            _ballController.Died += OnBallDied;
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