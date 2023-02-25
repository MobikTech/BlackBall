using BlackBall.Audio;
using BlackBall.Services;
using BlackBall.Services.SaveLoad;
using BlackBall.Services.SaveLoad.Serialization;
using UnityEngine;

namespace BlackBall
{
    public class ServiceLocator
    {
        public static readonly ServiceLocator ServiceLocatorInstance =
            new ServiceLocator(new PerGameData(), new PlayerData(), new SaveLoader(new BinarySaveSerializer()));

        public ServiceLocator(PerGameData perGameData, PlayerData playerData, ISaveLoader saveLoader)
        {
            PerGameData = perGameData;
            PlayerData = playerData;
            SaveLoader = saveLoader;
            SoundsPlayer = Object.FindObjectOfType<SoundsPlayer>();
            SceneLoader = Object.FindObjectOfType<SceneLoaderAsync>();
            SaveLoader.Load(null, PlayerData);
        }

        public PerGameData PerGameData { get; }
        public PlayerData PlayerData { get; }
        public ISaveLoader SaveLoader { get; }
        public SoundsPlayer SoundsPlayer { get; }
        public SceneLoaderAsync SceneLoader { get; }
    }
}