using System;
using System.Collections.Generic;
using System.Linq;
using BlackBall.Common.Math;
using BlackBall.Core;
using BlackBall.Factories;
using BlackBall.Factories.Core;
using BlackBall.Settings;
using UnityEngine;

namespace BlackBall
{
    public class PlatformsSpawner : CoreBehaviour
    {
        public List<PlatformBase> ActivePlatforms { get; private set; } = null!;
        
        [SerializeField] private Field _field = null!;
        [SerializeField] private List<PlatformSpawnSettingInfo> _spawnSettingsInfo = null!;

        private PlatformFactory _platformFactory = null!;
        private PlatformsSpawnSettings _currentPlatformsSpawnSettings;
        private int _nextSpawnSettings;
        
        private void Awake()
        {
            _platformFactory = new PlatformFactory();
            ActivePlatforms = new List<PlatformBase>();
            _field.PlatformLeavedGameField += OnPlatformLeavedField;
            _spawnSettingsInfo = _spawnSettingsInfo.OrderBy(info => info.OpensFromScore).ToList();
        }
        private void Update()
        {
            TrySwitchSpawnSettings();
            
            if (ActivePlatforms.Count == 0 || _field.CanSpawnNewPlatform(ActivePlatforms.Last().transform.position.y))
            {
                SpawnPlatform();
            }
        }

        private void OnPlatformLeavedField(PlatformBase platformBase)
        {
            _platformFactory.Delete(platformBase);
            ActivePlatforms.Remove(platformBase);
        }
        private void TrySwitchSpawnSettings()
        {
            if (_spawnSettingsInfo.Count > _nextSpawnSettings 
                && ServiceLocator.ServiceLocatorInstance.GameScore.Score >= _spawnSettingsInfo[_nextSpawnSettings].OpensFromScore)
            {
                _currentPlatformsSpawnSettings = _spawnSettingsInfo[_nextSpawnSettings].PlatformsSpawnSettings;
                _nextSpawnSettings++;
            }
        }
        private void SpawnPlatform()
        {
            PlatformBase prefab = SelectPlatformPrefab();
            Vector3 prefabSize = prefab.transform.localScale;
            PlatformBase newPlatform = _platformFactory.Create(prefab, new DefaultGOCreationOptions(
                _field.GetRandomPlatformPosition(prefabSize),
                Quaternion.identity,
                transform));
            ActivePlatforms.Add(newPlatform);
        }
        private PlatformBase SelectPlatformPrefab()
        {
            float totalProbability = 0f;
            foreach (PlatformInfo platformInfo in _currentPlatformsSpawnSettings.PlatformsInfo)
            {
                if (RandomAddition.IsTruth(platformInfo.Probability + totalProbability))
                {
                    return platformInfo.PlatformPrefab;
                }

                totalProbability += platformInfo.Probability;
            }

            throw new Exception("Platform prefab wasn't chosen");
        }
    }
}