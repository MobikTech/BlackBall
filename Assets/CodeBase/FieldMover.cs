using System;
using System.Collections;
using BlackBall.Platforms;
using UnityEngine;

namespace BlackBall
{
    public class FieldMover : MonoBehaviour
    {
        [SerializeField] private PlatformsSpawner _platformsSpawner = null!;
        [SerializeField] private float _fieldSpeed = 0.3f;
        // 100c = +0.1f
        [SerializeField] private float _speedMultiplier = 0.00002f;
        [SerializeField] private float _scoreFactor = 1.5f;
        
        private void Start()
        {
            _platformsSpawner.PlatformSpawned += OnPlatformSpawned;

            StartCoroutine(GameplayCoroutine());
        }

        private void FixedUpdate()
        {
            _fieldSpeed += _speedMultiplier;
        }

        private void OnPlatformSpawned(PlatformBase platform)
        {
            var rigidbody = platform.GetComponentInChildren<Rigidbody2D>();
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, _fieldSpeed); 
        }

        private IEnumerator GameplayCoroutine()
        {
            while (true)
            {
                float passedDistance = _fieldSpeed * Time.deltaTime * _scoreFactor;

                ServiceLocator.ServiceLocatorInstance.PerGameData.Score.UpdateScore(passedDistance);
                yield return null;
            }
        }
    }
}