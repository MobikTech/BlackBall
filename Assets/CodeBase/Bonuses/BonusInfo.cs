using System;

namespace BlackBall.Bonuses
{
    [Serializable]
    public struct BonusInfo
    {
        public BonusInfo(GatheringObject bonusPrefab, float probability)
        {
            BonusPrefab = bonusPrefab;
            Probability = probability;
        }

        public GatheringObject BonusPrefab;
        public float Probability;
    }
}