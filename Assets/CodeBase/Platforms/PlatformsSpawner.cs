using System;
using System.Collections.Generic;
using System.Linq;
using BlackBall.Bonuses;
using BlackBall.Factories;
using BlackBall.Settings;
using Mobik.Common.Core;
using Mobik.Common.Utilities.PoolingFactory.Core;
using UnityEngine;
using Random = Mobik.Common.Math.Random;

namespace BlackBall.Platforms
{
    public class PlatformsSpawner : MonoBehaviourCached
    {
        public event Action<PlatformBase>? PlatformSpawned;
        public PlatformBase LowestPlatform => _activePlatforms.Last();

        [SerializeField] private Field _field = null!;
        [SerializeField] private List<PlatformSpawnSettingInfo> _spawnSettingsInfo = null!;
        [SerializeField] private BonusSpawner _bonusSpawner = null!;

        private List<PlatformBase> _activePlatforms = null!;
        private PlatformFactory _platformFactory = null!;
        private PlatformsSpawnSettings _currentPlatformsSpawnSettings;
        private int _nextSpawnSettings;
        
        private void Awake()
        {
            _platformFactory = new PlatformFactory();
            _activePlatforms = new List<PlatformBase>();
            _activePlatforms.AddRange(GetComponentsInChildren<PlatformBase>());
            _field.PlatformLeavedGameField += OnPlatformLeavedField;
            _spawnSettingsInfo = _spawnSettingsInfo.OrderBy(info => info.OpensFromScore).ToList();
        }
        private void Update()
        {
            TrySwitchSpawnSettings();
            
            if (_activePlatforms.Count == 0 || _field.CanSpawnNewPlatform(_activePlatforms.Last().transform.position.y))
            {
                SpawnPlatform();
            }
        }

        private void OnPlatformLeavedField(PlatformBase platformBase)
        {
            _platformFactory.Delete(platformBase);
            _activePlatforms.Remove(platformBase);
        }
        private void TrySwitchSpawnSettings()
        {
            if (_spawnSettingsInfo.Count > _nextSpawnSettings 
                && ServiceLocator.ServiceLocatorInstance.PerGameData.Score.DistanceScore >= _spawnSettingsInfo[_nextSpawnSettings].OpensFromScore)
            {
                _currentPlatformsSpawnSettings = _spawnSettingsInfo[_nextSpawnSettings].PlatformsSpawnSettings;
                _nextSpawnSettings++;
            }
        }
        private void SpawnPlatform()
        {
            PlatformBase prefab = SelectPlatformPrefab();
            Vector3 prefabSize = prefab.transform.localScale;
            DefaultGOCreationOptions<PlatformBase> creationOptions = new DefaultGOCreationOptions<PlatformBase>(
                prefab,
                _field.GetRandomPlatformPosition(prefabSize),
                Quaternion.identity,
                transform);
            
            PlatformBase newPlatform = _platformFactory.Create(creationOptions);
            _activePlatforms.Add(newPlatform);
            _bonusSpawner.TrySpawnBonus(newPlatform);
            PlatformSpawned?.Invoke(newPlatform);
        }
        private PlatformBase SelectPlatformPrefab()
        {
            float totalProbability = 0f;
            foreach (PlatformInfo platformInfo in _currentPlatformsSpawnSettings.PlatformsInfo)
            {
                if (Random.IsTruth(platformInfo.Probability + totalProbability))
                {
                    return platformInfo.PlatformPrefab;
                }

                totalProbability += platformInfo.Probability;
            }

            throw new Exception("Platform prefab wasn't chosen");
        }
    }
}