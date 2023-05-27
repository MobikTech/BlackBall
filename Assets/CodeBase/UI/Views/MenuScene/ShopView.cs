using System;
using Mobik.Common.Core;
using Mobik.Common.Utilities.UIFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlackBall.UI.Views.MenuScene
{
    public class ShopView : UIView
    {
        public override Type ActualType => typeof(ShopView);

        [SerializeField] private Button _backButton = null!;
        [SerializeField] private TMP_Text _moneyText = null!;

        public override void Initialize()
        {
            _backButton.onClick.AddListener(GoBack);
            ServiceLocator.ServiceLocatorInstance.PlayerData.MoneyUpdated += OnMoneyUpdated;
            _moneyText.text = ServiceLocator.ServiceLocatorInstance.PlayerData.PlayerTotalMoney.ToString();
        }

        private void GoBack()
        {
            _viewVisualizer.Hide<ShopView>();
            _viewVisualizer.Visualize<StartView, IOptions>(IOptions.NoneOptions);
        }

        private void OnMoneyUpdated(int newMoney) => _moneyText.text = newMoney.ToString();
        private void OnDestroy() => ServiceLocator.ServiceLocatorInstance.PlayerData.MoneyUpdated -= OnMoneyUpdated;
    }
}