using System;
using UnityEngine;

namespace BlackBall
{
    public class ScreenRelativeScaler : MonoBehaviour
    {
        [SerializeField] private ScaleType _scaleType = ScaleType.Both;
        [SerializeField] private float _scaleFactor = 1f;

        private void Start()
        {
            float width = ScreenSize.GetScreenToWorldWidth();
            float height = ScreenSize.GetScreenToWorldHeight();
            
            // transform.localScale = new Vector3(width * _scale.x, height * _scale.y, 1f);
            transform.localScale = _scaleType switch
            {
                ScaleType.Width => new Vector3(width * _scaleFactor, width * _scaleFactor, 1f),
                ScaleType.Height => new Vector3(height * _scaleFactor, height * _scaleFactor, 1f),
                ScaleType.Both => new Vector3(width * _scaleFactor, height * _scaleFactor, 1f),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public enum ScaleType
    {
        Width,
        Height,
        Both,
    }
}