﻿using System;
using BlackBall.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BlackBall
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Field : CoreBehaviour
    {
        public event Action<PlatformBase>? PlatformLeavedGameField;
        
        [SerializeField] private float _platformBetweenDistance = 1f;
      
        private BoxCollider2D _boxCollider2D = null!;

        public bool CanSpawnNewPlatform(float lastPlatformHeight)
        {
            float bottomFieldPosition = transform.position.y - _boxCollider2D.size.y / 2;
            return Mathf.Abs(bottomFieldPosition - lastPlatformHeight) >= _platformBetweenDistance;
        }

        public Vector3 GetRandomPlatformPosition(Vector2 platformSize)
        {
            var xPos = Random.Range(
                transform.position.x - _boxCollider2D.size.x / 2 + platformSize.x / 2,
                transform.position.x + _boxCollider2D.size.x / 2 - platformSize.x / 2);
            float yPos = transform.position.y - _boxCollider2D.size.y / 2 - platformSize.y / 2;
            return new Vector3(xPos, yPos, 0f);
        }

        private void Awake()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
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