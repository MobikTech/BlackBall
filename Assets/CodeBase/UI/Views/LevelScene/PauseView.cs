using System;
using BlackBall.UI.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BlackBall.UI.Views.LevelScene
{
    public class PauseView : UIView
    {
        public override Type ActualType => typeof(PauseView);
        
        [SerializeField] private Button _continueButton = null!;
        [SerializeField] private Button _leaveButton = null!;

        internal override void Initialize(IViewVisualizer viewVisualizer)
        {
            base.Initialize(viewVisualizer);
            _continueButton.onClick.AddListener(ContinueGame);
            _leaveButton.onClick.AddListener(LeaveToMenu);
        }

        private void ContinueGame()
        {
            ServiceLocator.ServiceLocatorInstance.PerGameData.Pause.IsPaused = false;
            Close();
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

        private void OnNewSceneLoaded()
        {
            ServiceLocator.ServiceLocatorInstance.PerGameData.Reset();
        }
    }
}