using System;
using Mobik.Common.Utilities.PoolingFactory.Abstr;
using UnityEngine;

namespace BlackBall.Bonuses
{
    public readonly struct BonusCreationOptions : ICreationOptions<GatheringObject>
    {
        public readonly GatheringObject Prefab { get; }
        public readonly Vector3 SpawnPoint;
        public readonly Quaternion Rotation;
        public readonly Transform Parent;
        public readonly Action<GatheringObject> Despawn;

        public BonusCreationOptions(GatheringObject prefab, Vector3 spawnPoint, Quaternion rotation, Transform parent, Action<GatheringObject> despawn)
        {
            Prefab = prefab;
            Despawn = despawn;
            SpawnPoint = spawnPoint;
            Rotation = rotation;
            Parent = parent;
        }

        
        public void SetupCreationOptions(GatheringObject item)
        {
            item.transform.position = SpawnPoint;
            item.transform.rotation = Rotation;
            item.transform.parent = Parent;
            item.ObjectDespawned += Despawn;
        }
    }
}