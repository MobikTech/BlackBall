using System;

namespace BlackBall.Services.Ads
{
    public interface IAdsService
    {
        void ShowRewardedVideo(Action onAdsWatchingSucceed);
    }
}