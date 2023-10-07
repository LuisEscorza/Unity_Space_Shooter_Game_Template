
using UnityEngine;

public class ScreenLimit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyBase _))
            GameplayManager.Manager.GameOver();
        ObjectPoolManager.Manager.ReturnObjectToPool(collision.gameObject);
    }
}
