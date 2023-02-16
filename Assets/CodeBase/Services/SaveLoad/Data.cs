using System;

namespace BlackBall.Services.SaveLoad
{
    [Serializable]
    public struct Data
    {
        public int CurrentMoney;
        public int DistanceRecord;

        public static Data DefaultData = new Data()
        {
            CurrentMoney = 0,
            DistanceRecord = 0,
        };
    }
}