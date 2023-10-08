using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
        DontDestroyOnLoad(gameObject);

    }


    public event Action OnPlayerDied;
    public void TriggerOnPlayerDied() { OnPlayerDied?.Invoke(); }

    public event Action<int> OnPlayerHealthChanged;
    public void TriggerOnPlayerHealthChanged(int hp) { OnPlayerHealthChanged?.Invoke(hp); }

    public event Action<string> OnPickupActivated;
    public void TriggeOnPickupActivated(string message) { OnPickupActivated?.Invoke(message); }

    public event Action<int> OnEnemyDied;
    public void TriggerOnEnemyDied(int score) { OnEnemyDied?.Invoke(score); }

}
