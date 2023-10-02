using UnityEngine;

public class EnemyBase : Entity
{
    #region Fields
    [field: Header("Base Settings")]
    [SerializeField] private Weapon _weapon;
    [SerializeField] private int _scoreReward;
    public bool IsAlive { get; private set; } = true;
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
            ObjectPoolManager.Manager.ReturnObjectToPool(gameObject);
        }
    }
    #endregion

    #region Health Methods
    public override void Die()
    {
        SpawnPowerup();
        IsAlive = false;
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

    private void SpawnPowerup()
    {
        int chance = Random.Range(0, 99);
        if (chance < 45) //chance to spawn something at all
        {
            chance = Random.Range(0, 99);
            if (chance > 20)
            {
                chance = Random.Range(0, 99);
                if (chance < 33) //Blaster
                    ObjectPoolManager.Manager.SpawnObject(GameplayManager.Manager.Powerups[1], transform.position, transform.rotation, ObjectPoolManager.PoolType.Powerup);
                else if (chance < 62) //Bomber
                    ObjectPoolManager.Manager.SpawnObject(GameplayManager.Manager.Powerups[2], transform.position, transform.rotation, ObjectPoolManager.PoolType.Powerup);
                else //SideShip
                    ObjectPoolManager.Manager.SpawnObject(GameplayManager.Manager.Powerups[3], transform.position, transform.rotation, ObjectPoolManager.PoolType.Powerup);
            }
            else //Medkit
                ObjectPoolManager.Manager.SpawnObject(GameplayManager.Manager.Powerups[0], transform.position, transform.rotation, ObjectPoolManager.PoolType.Powerup);
        }
        else return;
    }
    #endregion
}
