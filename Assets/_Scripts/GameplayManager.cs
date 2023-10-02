using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    #region Fields
    [field: Header("Components")]
    [field: SerializeField] public GameplayHudManager HUDManager { get; private set; }
    [field: SerializeField] private EnemySpawnManager _enemySpawnManager;

    [field: Header("Misc")]
    [field: SerializeField] public GameObject[] Powerups { get; private set; }
    public int Score { get; private set; } = 0;
    public int GameplayTime { get; private set; } = 0;
    public static GameplayManager Manager { get; private set; }
    #endregion


    private void Awake()
    {
        if (Manager != null && Manager != this)
            Destroy(this);
        else
            Manager = this;
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        StartCoroutine(nameof(StartScoreTimer));
        _enemySpawnManager.StartSpawning();
    }

    private IEnumerator StartScoreTimer()
    {
        yield return new WaitForSeconds(1f);
        GameplayTime++;
        while (true)
        {
            GameplayTime++;
            IncreaseScore(1);
            switch (GameplayTime)
            {
                case 30: _enemySpawnManager.SpawningWaitTime = 2f; break;
                case 70: _enemySpawnManager.SpawningWaitTime = 1f; break;
                case 130: _enemySpawnManager.SpawningWaitTime = 0.5f; break;
                default: break;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void StopScoreTimer()
    {
        StopCoroutine(nameof(StopScoreTimer));
    }

    public void IncreaseScore(int amount)
    {
        Score += amount;
        HUDManager.UpdateScoreText(Score);
    }

    public void GameOver()
    {
        StopScoreTimer();
        _enemySpawnManager.StopSpawning();
        HUDManager.ShowGameOver();
    }
}
