using System;
using BlackBall.Settings;

namespace BlackBall.Platforms
{
    [Serializable]
    public struct PlatformSpawnSettingInfo
    {
        public PlatformSpawnSettingInfo(PlatformsSpawnSettings platformsSpawnSettings, int opensFromScore)
        {
            PlatformsSpawnSettings = platformsSpawnSettings;
            OpensFromScore = opensFromScore;
        }

        public PlatformsSpawnSettings PlatformsSpawnSettings;
        public int OpensFromScore;
    }
}