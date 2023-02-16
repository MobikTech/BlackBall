using System;
using BlackBall.Services.SaveLoad;

namespace BlackBall.Services.PerGameServices
{
    public class Money : IResetableService, IPersistentData
    {
        public event Action<int>? MoneyUpdated;
        public int MoneyValue { get; private set; }

        public void UpdateMoney(int moneyAddition)
        {
            MoneyValue += moneyAddition;
            MoneyUpdated?.Invoke(MoneyValue);
        }

        public void Reset()
        {
            MoneyValue = 0;
        }

        public void LoadData(Data data)
        {
            throw new NotImplementedException();
        }

        public void SaveData(ref Data data)
        {
            data.CurrentMoney += MoneyValue;
        }
    }
}