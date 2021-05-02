using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField]
  private GameObjectRuntimeSet enemyList;
  [SerializeField]
  private GameObject enemyPrefabToSpawn;
  [SerializeField]
  private Tilemap obstacles;
  [SerializeField]
  private FloatVariable totalEnemiesToSpawn;
  [SerializeField]
  private float spawningDelay;
  [SerializeField]
  private List<Transform> spawnPoints;
  private bool isSpawningEnemy;
  private bool spawnEnemies;
  private float defaultTotalEnemiesToSpawn;

  private void Update()
  {
    this.TryToSpawnEnemy();
  }

  private void OnEnable()
  {
    this.Setup();
    this.StartSpawnEnemies();
  }

  private void Setup()
  {
    this.defaultTotalEnemiesToSpawn = this.totalEnemiesToSpawn.Value;
  }

  private void TryToSpawnEnemy()
  {
    if (!this.spawnEnemies)
    {
      return;
    }

    if (this.enemyList.items.Count < this.totalEnemiesToSpawn.Value && !this.isSpawningEnemy)
    {
      StartCoroutine(this.SpawnEnemy());
    }
  }

  private IEnumerator SpawnEnemy()
  {
    this.isSpawningEnemy = true;
    Transform spawnPoint = this.spawnPoints[Random.Range(0, this.spawnPoints.Count)];

    GameObject enemy = Instantiate(this.enemyPrefabToSpawn, this.GetSpawnPosition(spawnPoint), transform.rotation);

    yield return new WaitForSeconds(this.spawningDelay);

    this.isSpawningEnemy = false;
  }

  private Vector2 GetSpawnPosition(Transform spawnPoint)
  {
    Vector2 origin = spawnPoint.position;
    Vector2 range = spawnPoint.localScale / 2.0f;
    Vector2 randomRange = new Vector2(Random.Range(-range.x, range.x),
                                     Random.Range(-range.y, range.y));
    Vector2 randomCoordinate = origin + randomRange;

    if (this.obstacles.HasTile(Vector3Int.RoundToInt(randomCoordinate)))
    {
      return this.GetSpawnPosition(spawnPoint);
    }

    return randomCoordinate;
  }

  public void Reset()
  {
    for (int i = 0; i < this.enemyList.items.Count; i++)
    {
      GameObject enemy = this.enemyList.items[i];

      if (enemy != null)
      {
        Destroy(enemy);
      }
    }

    this.enemyList.items = new List<GameObject>();
    this.totalEnemiesToSpawn.Value = this.defaultTotalEnemiesToSpawn;
  }

  private void StartSpawnEnemies()
  {
    this.Reset();
    this.spawnEnemies = true;
    this.isSpawningEnemy = false;
  }

  public void StopSpawnEnemies()
  {
    this.spawnEnemies = false;
  }
}
