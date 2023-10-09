using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.Events;

public class GameplayManager : MonoBehaviour
{
    #region Fields
    [field: Header("Events")]
    [field: SerializeField] private UnityEvent _onPlayerDied;
    [field: SerializeField] private UnityEvent<int> _onScoreIncreased;
    [field: SerializeField] private UnityEvent<int> _onGameplayTimerIncreased;
    [field: SerializeField] private UnityEvent _onGameStarted;

    [field: Header("Misc")]
    public int Score { get; private set; } = 0;
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
        _onGameStarted?.Invoke();
    }

    private IEnumerator StartScoreTimer()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            _onGameplayTimerIncreased?.Invoke(1);
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
