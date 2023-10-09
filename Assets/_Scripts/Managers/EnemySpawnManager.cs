using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    #region Fields
    [field: Header("Spawning Settings")]
    [field: SerializeField] private GameObject[] _enemyShipPrefabs;
    [field: SerializeField] private Transform[] _spawnPositions;
    private float _spawningWaitTime = 3f;
    private float _spawningElapsedTime = 0f;
    #endregion

    public void StartSpawning()
    {
        StartCoroutine(nameof(SpawnEnemiesOverTime));
    }

    public void StopSpawning()
    {
        StopCoroutine(nameof(SpawnEnemiesOverTime));
    }


    public void GameplayTimeUpdated(int gameplayTime)
    {
        _spawningElapsedTime += gameplayTime;
        switch (_spawningElapsedTime)
        {
            case 30: _spawningWaitTime = 2f; break;
            case 70: _spawningWaitTime = 1f; break;
            case 130: _spawningWaitTime = 0.5f; break;
            default: break;
        }
    }

    private IEnumerator SpawnEnemiesOverTime()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            int randomID = Random.Range(0, _spawnPositions.Length);
            if (_spawningElapsedTime < 30)
                ObjectPoolManager.Instance.SpawnObject(_enemyShipPrefabs[0], _spawnPositions[randomID].position, _spawnPositions[randomID].rotation, ObjectPoolManager.PoolType.EnemyShip);
            else if (_spawningElapsedTime < 60)
                ObjectPoolManager.Instance.SpawnObject(_enemyShipPrefabs[1], _spawnPositions[randomID].position, _spawnPositions[randomID].rotation, ObjectPoolManager.PoolType.EnemyShip);
            else
                ObjectPoolManager.Instance.SpawnObject(_enemyShipPrefabs[2], _spawnPositions[randomID].position, _spawnPositions[randomID].rotation, ObjectPoolManager.PoolType.EnemyShip);

            yield return new WaitForSeconds(_spawningWaitTime);
        }
    }
}
