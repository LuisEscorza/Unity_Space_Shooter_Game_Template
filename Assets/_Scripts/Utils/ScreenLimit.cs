
using UnityEngine;

public class ScreenLimit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyBase _))
            EventManager.Instance.TriggerOnPlayerDied();
        ObjectPoolManager.Instance.ReturnObjectToPool(collision.gameObject);
    }
}
