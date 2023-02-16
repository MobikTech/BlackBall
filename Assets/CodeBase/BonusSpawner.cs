using System.Collections.Generic;
using System.Linq;
using BlackBall.Bonuses;
using BlackBall.Common.Math;
using BlackBall.Core;
using BlackBall.Factories;
using BlackBall.Services.PerGameServices;
using BlackBall.Settings;
using UnityEngine;

namespace BlackBall
{
    public class BonusSpawner  : CoreBehaviour
    {
        [SerializeField] private List<BonusSpawnSettingInfo> _spawnSettingsInfo = null!;
        
        private BonusFactory _bonusFactory = null!;
        private BonusesSpawnSettings _currentBonusesSpawnSettings = null!;
        private Score _score = null!;
        private int _nextSettingsIndex = 0;

        private void Awake()
        {
            _spawnSettingsInfo = _spawnSettingsInfo.OrderBy(settings => settings.OpensFromScore).ToList();
            _bonusFactory = new BonusFactory();
            _score = ServiceLocator.ServiceLocatorInstance.PerGameData.Score;
            _currentBonusesSpawnSettings = _spawnSettingsInfo[_nextSettingsIndex].BonusesSpawnSettings;
            _nextSettingsIndex++;
        }

        private void Update()
        {
            TrySwitchSpawnSettings();
        }

        public bool TrySpawnBonus(PlatformBase platform)
        {
            if (!platform.CanSpawnBonuses || !TrySelectBonusPrefab(out GatheringObject? bonusPrefab))
            {
                return false;
            }

            _bonusFactory.Create(bonusPrefab!, new BonusCreationOptions(platform.BonusSpawnPoint.position, Quaternion.identity,
                platform.transform, bonus => _bonusFactory.Delete(bonus)));
            return true;
        }

        private void TrySwitchSpawnSettings()
        {
            if (_spawnSettingsInfo.Count > _nextSettingsIndex 
                && _score.DistanceScore >= _spawnSettingsInfo[_nextSettingsIndex].OpensFromScore)
            {
                _currentBonusesSpawnSettings = _spawnSettingsInfo[_nextSettingsIndex].BonusesSpawnSettings;
                _nextSettingsIndex++;
            }
        }
        
        private bool TrySelectBonusPrefab(out GatheringObject? bonusPrefab)
        {
            float totalProbability = 0f;
            foreach (BonusInfo bonusInfo in _currentBonusesSpawnSettings.BonusesInfo)
            {
                if (RandomAddition.IsTruth(bonusInfo.Probability + totalProbability))
                {
                    bonusPrefab = bonusInfo.BonusPrefab;
                    return true;
                }

                totalProbability += bonusInfo.Probability;
            }

            bonusPrefab = null;
            return false;
        }
    }
}