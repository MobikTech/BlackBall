using BlackBall.Core;
using UnityEngine;

namespace BlackBall.Factories.Core
{
    public struct DefaultGOCreationOptions : IOptions
    {
        public Vector3 SpawnPoint;
        public Quaternion Rotation;
        public Transform Parent;

        public DefaultGOCreationOptions(Vector3 spawnPoint, Quaternion rotation, Transform parent)
        {
            SpawnPoint = spawnPoint;
            Rotation = rotation;
            Parent = parent;
        }
    }
}