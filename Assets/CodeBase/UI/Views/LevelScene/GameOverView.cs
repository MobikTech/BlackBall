using System;
using BlackBall.UI.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BlackBall.UI.Views.LevelScene
{
    public class GameOverView : UIView
    {
        public override Type ActualType => typeof(GameOverView);
        
        [SerializeField] private Button _retryButton = null!;
        [SerializeField] private Button _menuButton = null!;

        internal override void Initialize(IViewVisualizer viewVisualizer)
        {
            base.Initialize(viewVisualizer);
            _retryButton.onClick.AddListener(RetryGame);
            _menuButton.onClick.AddListener(LeaveToMenu);
        }

        private void RetryGame()
        {
            ServiceLocator.ServiceLocatorInstance.SaveLoader.Save(
                null, ServiceLocator.ServiceLocatorInstance.PerGameData);
            ServiceLocator.ServiceLocatorInstance.SaveLoader.Load(
                null, ServiceLocator.ServiceLocatorInstance.PlayerData);
            
            ServiceLocator.ServiceLocatorInstance.PerGameData.Reset();
            ServiceLocator.ServiceLocatorInstance.SceneLoader.Load("Level");
        }

        private void LeaveToMenu()
        {
            ServiceLocator.ServiceLocatorInstance.SaveLoader.Save(
                null, ServiceLocator.ServiceLocatorInstance.PerGameData);
            ServiceLocator.ServiceLocatorInstance.SaveLoader.Load(
                null, ServiceLocator.ServiceLocatorInstance.PlayerData);
            
            ServiceLocator.ServiceLocatorInstance.PerGameData.Reset();
            ServiceLocator.ServiceLocatorInstance.SceneLoader.Load("Menu");
        }
    }
}