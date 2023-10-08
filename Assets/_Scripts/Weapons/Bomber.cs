using System.Collections;

public class Bomber : Weapon
{
    public override IEnumerator WeaponFireAction()
    {
        while (true)
        {
            if (AttackTimer >= FireRate)
            {
                Projectile[] newProjectiles = new Projectile[Amount];
                switch (Amount)
                {
                    case 1:
                        newProjectiles[0] = ObjectPoolManager.Instance.SpawnObject(_projectilePrefab, _projectileSpawnPoints[1].position, _projectileSpawnPoints[1].rotation, ObjectPoolManager.PoolType.PlayerProjectile).GetComponent<Projectile>();
                        PassProjectileStats(newProjectiles[0]); break;
                    case 2:
                        for (int i = 0; i < Amount; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    newProjectiles[i] = ObjectPoolManager.Instance.SpawnObject(_projectilePrefab, _projectileSpawnPoints[1].position, _projectileSpawnPoints[1].rotation, ObjectPoolManager.PoolType.PlayerProjectile).GetComponent<Projectile>(); ; break;
                                case 1:
                                    newProjectiles[i] = ObjectPoolManager.Instance.SpawnObject(_projectilePrefab, _projectileSpawnPoints[2].position, _projectileSpawnPoints[2].rotation, ObjectPoolManager.PoolType.PlayerProjectile).GetComponent<Projectile>(); ; break;
                            }
                            PassProjectileStats(newProjectiles[i]);
                        }
                        break;
                    case 3:
                        for (int i = 0; i < Amount; i++)
                        {
                            newProjectiles[i] = ObjectPoolManager.Instance.SpawnObject(_projectilePrefab, _projectileSpawnPoints[i].position, _projectileSpawnPoints[i].rotation, ObjectPoolManager.PoolType.PlayerProjectile).GetComponent<Projectile>(); ;
                            PassProjectileStats(newProjectiles[i]);
                        }
                        break;
                    case 4:
                        for (int i = 0; i < Amount; i++)
                        {
                            newProjectiles[i] = ObjectPoolManager.Instance.SpawnObject(_projectilePrefab, _projectileSpawnPoints[i].position, _projectileSpawnPoints[i].rotation, ObjectPoolManager.PoolType.PlayerProjectile).GetComponent<Projectile>(); ;
                            PassProjectileStats(newProjectiles[i]);
                        }
                        break;
                }
                AudioManager.Instance.PlaySFX(FireAudio);
                ResetAttackTimer();
            }
            yield return null;
        }
    }

    public override void IncreaseLevel()
    {
        base.IncreaseLevel();
        if (IsMaxLevelReached == false)
        {
            switch (Level)
            {
                case 2: IncreaseFireRate(0.2f); break;
                case 3: IncreaseAmount(1); break;
                case 4: IncreaseFireRate(0.3f); break;
                case 5: IncreaseDamageValue(4, 0.3f); break;
                case 6: IncreaseFireRate(0.3f); break;
                case 7: IncreaseAmount(1); break;
                case 8: IncreaseDamageValue(10, 0.3f); break;
                case 9: IncreaseFireRate(0.3f); break;
                case 10: IncreaseAmount(1); break;
                default: break;
            }
        }
    }
}
