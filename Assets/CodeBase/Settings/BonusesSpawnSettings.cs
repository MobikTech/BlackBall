using System.Collections.Generic;
using BlackBall.Bonuses;
using UnityEngine;

namespace BlackBall.Settings
{
    [CreateAssetMenu(fileName = "BonusSpawnSettings", menuName = "Bonus Spawn Settings", order = 0)]
    public class BonusesSpawnSettings : ScriptableObject
    {
        public List<BonusInfo> BonusesInfo => _bonusesInfo;
        
        [SerializeField] private List<BonusInfo> _bonusesInfo = null!;
    }
}