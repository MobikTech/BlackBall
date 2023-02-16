using BlackBall.UI.Core;
using TMPro;
using UnityEngine;

namespace BlackBall.UI.Widgets
{
    public class MoneyWidget : UIWidget
    {
        [SerializeField] private TMP_Text _moneyText = null!;

        internal override void Initialize()
        {
            base.Initialize();
            ServiceLocator.ServiceLocatorInstance.PerGameData.Money.MoneyUpdated += OnScoreUpdated;
        }

        private void OnScoreUpdated(int money)
        {
            _moneyText.text = money.ToString();
        }
    }
}