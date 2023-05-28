using System;
using Mobik.Common.Utilities.UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace BlackBall.UI.Widgets
{
    [RequireComponent(typeof(Button))]
    public class ContinueWithAdsWidget : UIWidget
    {
        public event Action? AdsCompleted;
        [SerializeField] private BallController _ballController;
        public override void Initialize()
        {
            GetComponent<Button>().onClick.AddListener(() => ServiceLocator.ServiceLocatorInstance.AdsService.ShowRewardedVideo(OnAdsWatchingSucceed));
        }

        private void OnAdsWatchingSucceed()
        {
            UnityEngine.Debug.Log("REVIVAL");
            _ballController.RevivePlayerBall();
            AdsCompleted?.Invoke();
        }
    }
}