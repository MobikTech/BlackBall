using System;
using Mobik.Common.Utilities.UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace BlackBall.UI.Views.LevelScene
{
    public class GameOverView : UIView
    {
        public override Type ActualType => typeof(GameOverView);

        [SerializeField] private Button _retryButton = null!;
        [SerializeField] private Button _menuButton = null!;

        public override void Initialize()
        {
            _retryButton.onClick.AddListener(RetryGame);
            _menuButton.onClick.AddListener(LeaveToMenu);
        }

        private void RetryGame()
        {
            BeforeNewSceneLoaded();
            ServiceLocator.ServiceLocatorInstance.SceneLoader.Load("Level", OnNewSceneLoaded);
        }

        private void LeaveToMenu()
        {
            BeforeNewSceneLoaded();
            ServiceLocator.ServiceLocatorInstance.SceneLoader.Load("Menu", OnNewSceneLoaded);
        }

        private void BeforeNewSceneLoaded()
        {
            ServiceLocator.ServiceLocatorInstance.SaveLoader.Save(
                null, ServiceLocator.ServiceLocatorInstance.PerGameData);
            ServiceLocator.ServiceLocatorInstance.SaveLoader.Load(
                null, ServiceLocator.ServiceLocatorInstance.PlayerData);
        }
        private void OnNewSceneLoaded() => ServiceLocator.ServiceLocatorInstance.PerGameData.Reset();
    }
}