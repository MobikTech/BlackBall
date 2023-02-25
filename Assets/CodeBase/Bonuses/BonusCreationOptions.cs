using System;
using BlackBall.Core;
using UnityEngine;

namespace BlackBall.Bonuses
{
    public struct BonusCreationOptions : IOptions
    {
        public Vector3 SpawnPoint;
        public Quaternion Rotation;
        public Transform Parent;
        public Action<GatheringObject> Despawn;

        public BonusCreationOptions(Vector3 spawnPoint, Quaternion rotation, Transform parent, Action<GatheringObject> despawn)
        {
            Despawn = despawn;
            SpawnPoint = spawnPoint;
            Rotation = rotation;
            Parent = parent;
        }
    }
}