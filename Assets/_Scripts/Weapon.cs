using System.Collections;
using UnityEngine;
public abstract class Weapon : MonoBehaviour
{
    #region Fields
    [field: Header("Base Stats")]
    [field: SerializeField] public string WeaponName { get; private set; }
    [field: SerializeField] private int _baseDamageValue;
    [field: SerializeField] private float _baseFireRate;
    [field: SerializeField] private float _baseProjectileSpeed;
    [field: SerializeField] private Vector3 _baseProjectileScale;
    [field: SerializeField] private int _baseLevel;
    [field: SerializeField] public int MaxLevel { get; private set; }
    [field: SerializeField] internal GameObject _projectilePrefab;
    [field: SerializeField] internal Transform[] _projectileSpawnPoints;

    [field: Header("Internal Stats")]
    public int DamageValue { get; private set; }
    public float FireRate { get; private set; }
    public float ProjectileSpeed { get; private set; }
    public Vector3 ProjectileScale { get; private set; }
    public int Amount { get; private set; } = 1;
    public float Level { get; private set; }
    public bool IsMaxLevelReached { get; private set; } = false;
    public float AttackTimer { get; private set; }

    [field: Header("FX")]
    [field: SerializeField] public AudioClip FireAudio { get; private set; }
    [field: SerializeField] public AudioClip HitAudio { get; private set; }
    #endregion

    #region Misc Methods
    public virtual void Awake()
    {
        Level = _baseLevel;
        DamageValue = _baseDamageValue;
        FireRate = _baseFireRate;
        ProjectileSpeed = _baseProjectileSpeed;
        ProjectileScale = _baseProjectileScale;
        if (Level == 0) gameObject.SetActive(false);
    }

    protected void PassProjectileStats(Projectile projectile)
    {
        projectile.SetProjectileStats(ProjectileScale, DamageValue, ProjectileSpeed, HitAudio);
    }

    private void Update()
    {
        AttackTimer += Time.deltaTime;
    }
    public abstract IEnumerator WeaponFireAction();

    public void StopWeaponAction()
    {
        StopCoroutine(nameof(WeaponFireAction));
    }

    public void StartWeaponAction()
    {
        StartCoroutine(nameof(WeaponFireAction));
    }

    #endregion

    #region Stats Modifying Methods
    public virtual void IncreaseLevel()
    {
        if (IsMaxLevelReached == true) return;

        gameObject.SetActive(true);
        if (Level < MaxLevel)
            Level++;
        else IsMaxLevelReached = true;

    }
    protected void IncreaseDamageValue(int value, float sizeIncrease)
    {
        DamageValue += value;
        ProjectileScale += new Vector3(sizeIncrease, sizeIncrease, 0);
    }

    protected void DecreaseDamageValue(int value)
    {
        DamageValue -= value;
    }

    protected void IncreaseFireRate(float value)
    {
        FireRate -= value;
    }

    protected void IncreaseAmount(int value)
    {
        Amount += value;
    }
   
    protected void DrecreaseFireRate(float value)
    {
        FireRate += value;
    }

    protected void ResetAttackTimer()
    {
        AttackTimer = 0;
    }
    #endregion




}
