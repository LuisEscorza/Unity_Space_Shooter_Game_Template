using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    #region Fields
    [field: Header("Spawning Settings")]
    [field: SerializeField] private GameObject[] _enemyShipPrefabs;
    [field: SerializeField] private Transform[] _spawnPositions;
    [HideInInspector]public float SpawningWaitTime = 3;
    #endregion

    public void StartSpawning()
    {
        StartCoroutine(nameof(SpawnEnemiesOverTime));
    }

    public void StopSpawning()
    {
        StopCoroutine(nameof(SpawnEnemiesOverTime));
    }

    private IEnumerator SpawnEnemiesOverTime()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            int randomID = UnityEngine.Random.Range(0, _spawnPositions.Length);
            if (GameplayManager.Manager.GameplayTime < 30)
                ObjectPoolManager.Manager.SpawnObject(_enemyShipPrefabs[0], _spawnPositions[randomID].position, _spawnPositions[randomID].rotation, ObjectPoolManager.PoolType.EnemyShip);
            else if (GameplayManager.Manager.GameplayTime < 60)
                ObjectPoolManager.Manager.SpawnObject(_enemyShipPrefabs[1], _spawnPositions[randomID].position, _spawnPositions[randomID].rotation, ObjectPoolManager.PoolType.EnemyShip);
            else
                ObjectPoolManager.Manager.SpawnObject(_enemyShipPrefabs[2], _spawnPositions[randomID].position, _spawnPositions[randomID].rotation, ObjectPoolManager.PoolType.EnemyShip);

            yield return new WaitForSeconds(SpawningWaitTime);
        }
    }
}
