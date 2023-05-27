using Mobik.Common.Core;
using Mobik.Common.Utilities.PoolingFactory.Abstr;
using UnityEngine;

namespace BlackBall.Platforms
{
    public abstract class PlatformBase : MonoBehaviourCached, IPoolItem
    {
        public abstract string GetItemTypeKey { get; }
        [field:SerializeField] public bool CanSpawnBonuses { get; private set; }
        [field:SerializeField] public Transform BonusSpawnPoint { get; private set; } = null!;
    }
}