using System;
using BlackBall.Settings;

namespace BlackBall.Bonuses
{
    [Serializable]
    public struct BonusSpawnSettingInfo
    {
        public BonusSpawnSettingInfo(BonusesSpawnSettings bonusesSpawnSettings, int opensFromScore)
        {
            BonusesSpawnSettings = bonusesSpawnSettings;
            OpensFromScore = opensFromScore;
        }

        public BonusesSpawnSettings BonusesSpawnSettings;
        public int OpensFromScore;
    }
}