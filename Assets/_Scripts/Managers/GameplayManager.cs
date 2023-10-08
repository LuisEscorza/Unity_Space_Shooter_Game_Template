using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.Events;

public class GameplayManager : MonoBehaviour
{
    #region Fields
    [field: Header("Components")]
    [field: SerializeField] private EnemySpawnManager _enemySpawnManager;


    [field: Header("Events")]
    [field: SerializeField] private UnityEvent _onPlayerDied;
    [field: SerializeField] private UnityEvent<int> _onScoreIncreased;
    [field: SerializeField] private UnityEvent<int> _onGameplayTimerIncreased;

    [field: Header("Misc")]
    public int Score { get; private set; } = 0;
    public int GameplayTime { get; private set; } = 0;
    #endregion


    private void OnEnable()
    {
        EventManager.Instance.OnPlayerDied += PlayerDied;
        EventManager.Instance.OnEnemyDied += IncreaseScore;
    }
    public void OnDisable()
    {
        EventManager.Instance.OnPlayerDied -= PlayerDied;
        EventManager.Instance.OnEnemyDied -= IncreaseScore;
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
            _onGameplayTimerIncreased?.Invoke(GameplayTime);
            IncreaseScore(1);
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
        _onScoreIncreased?.Invoke(Score);
    }

    public void PlayerDied()
    {
        _onPlayerDied?.Invoke();
        StopScoreTimer();
    }
}
