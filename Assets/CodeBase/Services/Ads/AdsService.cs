using System;
using BlackBall.Settings;
using UnityEngine;
using UnityEngine.Advertisements;

namespace BlackBall.Services.Ads
{
    public class AdsService : MonoBehaviour, IAdsService, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField] private AdsConfiguration _adsConfiguration = null!;
        
        private Action? _onAdsWatchingSucceed;

        private void Awake()
        {
            Advertisement.Initialize(_adsConfiguration.GameID, _adsConfiguration.InTestMode, this);
            Advertisement.Load(_adsConfiguration.RewardedAdPlacementID, this);
        }

        public void ShowRewardedVideo(Action? onAdsWatchingSucceed)
        {
            _onAdsWatchingSucceed = onAdsWatchingSucceed;
            Advertisement.Show(_adsConfiguration.RewardedAdPlacementID, this);
        }

        public void OnInitializationComplete()
        {
            UnityEngine.Debug.Log($"Ads initialization completed");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            UnityEngine.Debug.Log($"Ads initialization failed with error '{error.ToString()}' - {message}");
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            UnityEngine.Debug.Log($"New Ad loaded");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            UnityEngine.Debug.Log($"New Ad loading failed with error '{error.ToString()}' - {message}");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            UnityEngine.Debug.Log($"Ads showing failed with error '{error.ToString()}' - {message}");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            UnityEngine.Debug.Log($"Ads showing started");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            UnityEngine.Debug.Log($"Ads showing completed with state {showCompletionState.ToString()}");
            if (showCompletionState != UnityAdsShowCompletionState.COMPLETED) return;
            
            _onAdsWatchingSucceed?.Invoke();
            Advertisement.Load(_adsConfiguration.RewardedAdPlacementID, this);
        }
    }
}