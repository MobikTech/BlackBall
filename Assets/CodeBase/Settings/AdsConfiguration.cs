using UnityEngine;

namespace BlackBall.Settings
{
    [CreateAssetMenu(menuName = "Create AdsConfiguration", fileName = "AdsConfiguration", order = 0)]
    public class AdsConfiguration : ScriptableObject
    {
        public bool InTestMode => _inTestMode;
        public string GameID => _gameID;
        public string RewardedAdPlacementID => _rewardedAdPlacementID;
        
        [SerializeField] private bool _inTestMode = true;
        [SerializeField] private string _gameID = "5293401";
        [SerializeField] private string _rewardedAdPlacementID = "Rewarded_Android";
    }
}