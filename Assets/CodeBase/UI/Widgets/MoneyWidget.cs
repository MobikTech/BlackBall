using Mobik.Common.Utilities.UIFramework;
using TMPro;
using UnityEngine;

namespace BlackBall.UI.Widgets
{
    public class MoneyWidget : UIWidget
    {
        [SerializeField] private TMP_Text _moneyText = null!;

        public override void Initialize()
        {
            ServiceLocator.ServiceLocatorInstance.PerGameData.Money.MoneyUpdated += OnScoreUpdated;
        }

        private void OnScoreUpdated(int money)
        {
            _moneyText.text = money.ToString();
        }
    }
}