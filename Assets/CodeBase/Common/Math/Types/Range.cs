using System;
using UnityEngine;

namespace BlackBall.Common.Math.Types
{
    [Serializable]
    public struct Range
    {
        [SerializeField] private int _min;
        [SerializeField] private int _max;
       
        public int Min => _min;
        public int Max => _max;

        public Range(int min, int max)
        {
            _min = min;
            _max = max;
        }
    }
}