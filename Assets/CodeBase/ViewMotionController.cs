using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlackBall
{
    public class ViewMotionController : MonoBehaviour
    {
        [SerializeField] private Transform _ball = null!;
        [SerializeField] private PlatformsManager _platformsManager = null!;
        [SerializeField] private float _speed = 0.5f;

        private void Update()
        {
            foreach (Transform movableObject in GetMovableObjects())
            {
                movableObject.Translate(Vector3.up * (_speed * Time.deltaTime));
            }
        }

        private List<Transform> GetMovableObjects()
        {
            return _platformsManager.ActivePlatforms
                .Select(platform => platform.transform)
                .Append(_ball)
                .ToList();
        }
    }
}