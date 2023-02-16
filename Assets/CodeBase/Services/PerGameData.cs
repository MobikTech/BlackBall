using System.Collections.Generic;
using BlackBall.Services.PerGameServices;
using BlackBall.Services.SaveLoad;

namespace BlackBall.Services
{
    public class PerGameData : IPersistentData
    {
        public Pause Pause { get; set; }
        public Score Score { get; set; }
        public Money Money { get; set; }

        private List<IResetableService> _resetableServices;

        public PerGameData()
        {
            Pause = new Pause();
            Score = new Score();
            Money = new Money();

            _resetableServices = new List<IResetableService>() { Pause, Score, Money };
        }

        public void Reset()
        {
            _resetableServices.ForEach(service => service.Reset());
        }

        public void LoadData(Data data)
        {
            throw new System.NotImplementedException();
        }

        public void SaveData(ref Data data)
        {
            data.CurrentMoney += Money.MoneyValue;
            if (Score.DistanceScore > data.DistanceRecord)
            {
                data.DistanceRecord = Score.DistanceScore;
            }
        }
    }
}