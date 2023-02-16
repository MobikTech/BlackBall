using System;
using BlackBall.Services.SaveLoad;

namespace BlackBall.Services
{
    public class PlayerData : IPersistentData
    {
        public event Action<int>? MoneyUpdated;
        public event Action<int>? RecordUpdated;
        public int PlayerTotalMoney { get; private set; }
        public int PlayerRecordDistance { get; private set; }

        public void LoadData(Data data)
        {
            if (PlayerTotalMoney != data.CurrentMoney)
            {
                PlayerTotalMoney = data.CurrentMoney;
                MoneyUpdated?.Invoke(PlayerTotalMoney);
            }
            
            if (PlayerRecordDistance != data.DistanceRecord)
            {
                PlayerRecordDistance = data.DistanceRecord;
                RecordUpdated?.Invoke(PlayerRecordDistance);
            }
        }

        public void SaveData(ref Data data)
        {
            throw new System.NotImplementedException();
        }
    }
}