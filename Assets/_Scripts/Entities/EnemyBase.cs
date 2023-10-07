using UnityEngine;

public class EnemyBase : Entity
{
    #region Fields
    [field: Header("Enemy Settings")]
    [SerializeField] private Weapon _weapon;
    [SerializeField] private int _scoreReward;
    [SerializeField] private GameObject[] _pickups;
    private Rigidbody2D _rb;
    private bool _firstSpawn = true;
    #endregion

    #region Misc. Methods
    public override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = transform.up * MovementSpeed;
        if (_firstSpawn == true)
        {
            _weapon.StartWeaponAction();
            _firstSpawn = false;
        }
    }

    public void OnEnable()
    {
        ResetHealth();
        _weapon.StartWeaponAction();
        _rb.velocity = transform.up * MovementSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.TryGetComponent(out IDamageable damageable);
            damageable.TakeDamage(_weapon.DamageValue);
            AudioManager.Manager.PlaySFX(_weapon.HitAudio, 0.9f);
            Die();
        }
    }
    #endregion

    #region Health Methods
    public override void Die()
    {
        SpawnPickup();
        _weapon.StopWeaponAction();
        ObjectPoolManager.Manager.ReturnObjectToPool(gameObject);
        GameplayManager.Manager.IncreaseScore(_scoreReward);
    }

    public override void TakeDamage(int damageValue)
    {
        DecreaseHealth(damageValue);
        if (Health <= 0)
            Die();
    }

    private void SpawnPickup()
    {
        int chance = Random.Range(0, 99);
        if (chance < 45) //chance to spawn something at all
        {
            chance = Random.Range(0, 99);
            if (chance > 20)
            {
                chance = Random.Range(1, 4);
                ObjectPoolManager.Manager.SpawnObject(_pickups[chance], transform.position, transform.rotation, ObjectPoolManager.PoolType.Pickup);
            }
            else //Medkit
                ObjectPoolManager.Manager.SpawnObject(_pickups[0], transform.position, transform.rotation, ObjectPoolManager.PoolType.Pickup);
        }
        else return;
    }
    #endregion
}
