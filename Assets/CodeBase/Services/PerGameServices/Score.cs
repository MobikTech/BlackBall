using System;
using BlackBall.Services.SaveLoad;

namespace BlackBall.Services.PerGameServices
{
    public class Score : IResetableService, IPersistentData
    {
        public event Action<int>? ScoreUpdated;
        public int DistanceScore => (int)_passedDistance;
        private float _passedDistance;

        public void UpdateScore(float passedDistance)
        {
            _passedDistance += passedDistance;
            ScoreUpdated?.Invoke(DistanceScore);
        }

        public void Reset()
        {
            _passedDistance = 0f;
        }

        public void LoadData(Data data)
        {
            throw new NotImplementedException();
        }

        public void SaveData(ref Data data)
        {
            if (DistanceScore > data.DistanceRecord)
            {
                data.DistanceRecord = DistanceScore;
            }
        }
    }
}