using System;
using BlackBall;
using BlackBall.Services.SaveLoad;
using UnityEngine;

public class BallSpawner : MonoBehaviour, IPersistentData
{
  [SerializeField] private GameObject[] allPossiblePrefabs;
  private GameObject prefabToSpawn;
  [SerializeField] private Vector3 spawnPosition = Vector3.zero;

  public BallController? SpawnedBallController { get; private set; }

  public event Action<BallController> BallSpawned;
  public void Start()
  {
    ServiceLocator.ServiceLocatorInstance.SaveLoader.Load(null, this);
    if(prefabToSpawn != null)
    {
      GameObject spawnedBall = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
      SpawnedBallController = spawnedBall.GetComponent<BallController>();

      if (SpawnedBallController == null)
      {
        Debug.LogError("The spawned prefab doesn't have a BallController component!");
      }
    }
    else
    {
      Debug.LogWarning("Prefab to spawn is not set!");
    }
    BallSpawned?.Invoke(SpawnedBallController);
  }

  public void LoadData (Data data)
  {
    prefabToSpawn = allPossiblePrefabs[data.SelectedBallID];
  }

  public void SaveData (ref Data data)
  {
    throw new NotImplementedException();
  }
}