using System;
using BlackBall;
using UnityEngine;

public class BallSpawn : MonoBehaviour
{
  [SerializeField] private GameObject[] allPossiblePrefabs;
  private GameObject prefabToSpawn;
  [SerializeField] private Vector3 spawnPosition = Vector3.zero; // Позиция, где вы хотите спавнить префаб

  public BallController? SpawnedBallController { get; private set; }

  public event Action<BallController> BallSpawned;
  private void Start()
  {
    string selectedPrefabName = PlayerPrefs.GetString("SelectedBallPrefabName", "");
    if(!string.IsNullOrEmpty(selectedPrefabName))
    {
      prefabToSpawn = Array.Find(allPossiblePrefabs, prefab => prefab.name == selectedPrefabName);
    }
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
  
  public void SetPrefabToSpawn(GameObject newPrefab)
  {
    prefabToSpawn = newPrefab;
  }
}