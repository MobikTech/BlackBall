using System;
using BlackBall.Core;
using BlackBall.Platforms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BlackBall
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Field : CoreBehaviour
    {
        public event Action<PlatformBase>? PlatformLeavedGameField;
        
        [SerializeField] private float _platformBetweenDistance = 1f;
      
        public bool CanSpawnNewPlatform(float lastPlatformHeight)
        {
            float bottomFieldPosition = transform.position.y - transform.localScale.y / 2;
            return Mathf.Abs(bottomFieldPosition - lastPlatformHeight) >= _platformBetweenDistance;
        }

        public Vector3 GetRandomPlatformPosition(Vector2 platformSize)
        {
            var xPos = Random.Range(
                transform.position.x - transform.localScale.x / 2 + platformSize.x / 2,
                transform.position.x + transform.localScale.x / 2 - platformSize.x / 2);
            float yPos = transform.position.y - transform.localScale.y / 2 - platformSize.y / 2;
            return new Vector3(xPos, yPos, 0f);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlatformBase platform))
            {
                PlatformLeavedGameField?.Invoke(platform);
            }
        }
    }
}