using BlackBall.UI.Core;
using TMPro;
using UnityEngine;

namespace BlackBall.UI.Widgets
{
    public class ScoreWidget : UIWidget
    {
        [SerializeField] private TMP_Text _scoreText = null!;

        internal override void Initialize()
        {
            base.Initialize();
            ServiceLocator.ServiceLocatorInstance.PerGameData.Score.ScoreUpdated += OnScoreUpdated;
        }

        private void OnScoreUpdated(int score)
        {
            _scoreText.text = score + "m";
        }
    }
}