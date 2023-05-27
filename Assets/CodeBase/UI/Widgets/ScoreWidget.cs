using Mobik.Common.Utilities.UIFramework;
using TMPro;
using UnityEngine;

namespace BlackBall.UI.Widgets
{
    public class ScoreWidget : UIWidget
    {
        [SerializeField] private TMP_Text _scoreText = null!;

        public override void Initialize()
        {
            ServiceLocator.ServiceLocatorInstance.PerGameData.Score.ScoreUpdated += OnScoreUpdated;
        }

        private void OnScoreUpdated(int score)
        {
            _scoreText.text = score + "m";
        }
    }
}