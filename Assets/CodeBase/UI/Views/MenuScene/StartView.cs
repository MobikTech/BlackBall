using System;
using Mobik.Common.Core;
using Mobik.Common.Utilities.UIFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlackBall.UI.Views.MenuScene
{
    public class StartView : UIView
    {
        public override Type ActualType => typeof(StartView);

        [SerializeField] private Button _playButton = null!;
        [SerializeField] private Button _shopButton = null!;
        [SerializeField] private Button _exitButton = null!;
        [SerializeField] private TMP_Text _recordText = null!;
    
        public override void Initialize()
        {
            _playButton.onClick.AddListener(GoToLevel);
            _shopButton.onClick.AddListener(SwitchToShopView);
            _exitButton.onClick.AddListener(ExitGame);
            
            _recordText.text = "RECORD: " + ServiceLocator.ServiceLocatorInstance.PlayerData.PlayerRecordDistance.ToString() + "m";
        }

        private void GoToLevel()
        {
            ServiceLocator.ServiceLocatorInstance.SceneLoader.Load("Level");
        }

        private void SwitchToShopView()
        {
            _viewVisualizer.Hide<StartView>();
            _viewVisualizer.Visualize<ShopView, IOptions>(IOptions.NoneOptions);
        }

        private void ExitGame()
        {
            Application.Quit();
        }
    }
}