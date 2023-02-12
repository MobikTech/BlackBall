using System;

namespace BlackBall
{
    [Serializable]
    public struct PlatformInfo
    {
        public PlatformInfo(PlatformBase platformPrefab, float probability)
        {
            PlatformPrefab = platformPrefab;
            Probability = probability;
        }

        public PlatformBase PlatformPrefab;
        public float Probability;
    }
}