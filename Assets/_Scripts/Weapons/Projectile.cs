
using UnityEngine;

public class Projectile : MonoBehaviour, IDamageable
{
    #region Variables
    [field: Header("Other Stats")]
    [SerializeField] private TrailRenderer _trail;
    [HideInInspector]public bool IsQueuedForDeletion = false;
    private int _damageValue;
    private AudioClip _bodyHitSound;
    private float _speed;
    public Rigidbody2D _rigidBody { get; private set; }
    private readonly object lockObject = new();
    #endregion

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        IsQueuedForDeletion = false;
    }

    public void SetProjectileStats(Vector3 scale, int damage, float speed, AudioClip bodyHitSound)
    {
        _trail.Clear();
        transform.localScale.Set(transform.localScale.x * scale.x, transform.localScale.y * scale.y, transform.localScale.z);
        _damageValue = damage;
        _speed = speed;
        _bodyHitSound = bodyHitSound;
        _rigidBody.velocity = _speed * transform.up;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _trail.Clear();
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damageValue);
            AudioManager.Instance.PlaySFX(_bodyHitSound, 0.9f);
            QueueForDeletion();
        }
    }

    public void TakeDamage(int damageValue)
    {
        _trail.Clear();
        QueueForDeletion();
    }

    private void QueueForDeletion()
    {
        lock (lockObject)
        {
            if (!IsQueuedForDeletion)
            {
                IsQueuedForDeletion = true;
                ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
            }
        }
    }
}
