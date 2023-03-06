using DG.Tweening;
using UnityEngine;

namespace BlackBall
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 1f;
        [SerializeField] private Ease _ease = Ease.Linear;
        
        private void Awake()
        {
            var rot = new Vector3(0f, 0f, 360f);
            transform.DOLocalRotate(rot, 1 / _rotationSpeed)
                .SetEase(_ease)
                .SetRelative(true)
                .SetUpdate(true)
                .SetLoops(-1, LoopType.Restart);
        }
    }
}