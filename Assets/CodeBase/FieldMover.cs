using System.Collections;
using BlackBall.Platforms;
using UnityEngine;

namespace BlackBall
{
    public class FieldMover : MonoBehaviour
    {
        [SerializeField] private PlatformsSpawner _platformsSpawner = null!;
        [SerializeField] private float _fieldSpeed = 0.5f;
        
        private void Start()
        {
            _platformsSpawner.PlatformSpawned += OnPlatformSpawned;

            StartCoroutine(GameplayCoroutine());
        }

        private void OnPlatformSpawned(PlatformBase platform)
        {
            var rigidbody = platform.GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, _fieldSpeed); 
        }

        private IEnumerator GameplayCoroutine()
        {
            while (true)
            {
                float passedDistance = _fieldSpeed * Time.deltaTime;

                ServiceLocator.ServiceLocatorInstance.PerGameData.Score.UpdateScore(passedDistance);
                yield return null;
            }
        }
    }
}