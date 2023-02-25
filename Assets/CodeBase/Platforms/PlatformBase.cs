using BlackBall.Core;
using BlackBall.Factories.Core;
using UnityEngine;

namespace BlackBall.Platforms
{
    public abstract class PlatformBase : CoreBehaviour, IPoolItem<DefaultGOCreationOptions>
    {
        public abstract string GetItemTypeKey { get; }
        [field:SerializeField] public bool CanSpawnBonuses { get; private set; }
        [field:SerializeField] public Transform BonusSpawnPoint { get; private set; } = null!;

        public void SetupCreationOptions(DefaultGOCreationOptions creationOptions)
        {
            transform.parent = creationOptions.Parent;
            transform.position = creationOptions.SpawnPoint;
            transform.rotation = creationOptions.Rotation;
        }
    }
}