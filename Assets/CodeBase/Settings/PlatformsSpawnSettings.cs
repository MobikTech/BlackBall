using System.Collections.Generic;
using UnityEngine;

namespace BlackBall.Settings
{
    [CreateAssetMenu(fileName = "PlatformsSpawnSettings", menuName = "Platforms Spawn Settings", order = 0)]
    public class PlatformsSpawnSettings : ScriptableObject
    {
        public List<PlatformInfo> PlatformsInfo => _platformsInfo;
        
        [SerializeField] private List<PlatformInfo> _platformsInfo = null!;
    }
}