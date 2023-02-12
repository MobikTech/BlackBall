using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlackBall
{
    public class FieldMover : MonoBehaviour
    {
        [SerializeField] private Transform _ball = null!;
        [SerializeField] private PlatformsSpawner _platformsSpawner = null!;
        [SerializeField] private float _fieldSpeed = 0.5f;
        
        private void Start()
        {
            StartCoroutine(GameplayCoroutine());
        }

        private IEnumerator GameplayCoroutine()
        {
            while (true)
            {
                float passedDistance = _fieldSpeed * Time.deltaTime;
                foreach (Transform movableObject in GetMovableObjects())
                {
                    movableObject.Translate(Vector3.up * passedDistance);
                }

                ServiceLocator.ServiceLocatorInstance.GameScore.UpdateScore(passedDistance);
                yield return null;
            }
        }
        
        private List<Transform> GetMovableObjects()
        {
            return _platformsSpawner.ActivePlatforms
                .Select(platform => platform.transform)
                .Append(_ball)
                .ToList();
        }
    }
}