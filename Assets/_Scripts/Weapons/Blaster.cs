using System.Collections;

public class Blaster : Weapon
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
                        newProjectiles[0] = ObjectPoolManager.Manager.SpawnObject(_projectilePrefab, _projectileSpawnPoints[1].position, _projectileSpawnPoints[1].rotation, ObjectPoolManager.PoolType.PlayerProjectile).GetComponent<Projectile>();
                        PassProjectileStats(newProjectiles[0]); break;
                    case 2:
                        for (int i = 0; i < Amount; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    newProjectiles[i] = ObjectPoolManager.Manager.SpawnObject(_projectilePrefab, _projectileSpawnPoints[0].position, _projectileSpawnPoints[0].rotation, ObjectPoolManager.PoolType.PlayerProjectile).GetComponent<Projectile>(); break;
                                case 1:
                                    newProjectiles[i] = ObjectPoolManager.Manager.SpawnObject(_projectilePrefab, _projectileSpawnPoints[2].position, _projectileSpawnPoints[2].rotation, ObjectPoolManager.PoolType.PlayerProjectile).GetComponent<Projectile>(); break;
                            }
                            PassProjectileStats(newProjectiles[i]);
                        }
                        break;
                    case 3:
                        for (int i = 0; i < Amount; i++)
                        {
                            newProjectiles[i] = ObjectPoolManager.Manager.SpawnObject(_projectilePrefab, _projectileSpawnPoints[i].position, _projectileSpawnPoints[i].rotation, ObjectPoolManager.PoolType.PlayerProjectile).GetComponent<Projectile>();
                            PassProjectileStats(newProjectiles[i]);
                        }
                        break;
                }
                AudioManager.Manager.PlaySFX(FireAudio);
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
                case 2: IncreaseFireRate(0.1f); break;
                case 3: IncreaseAmount(1); break;
                case 4: IncreaseDamageValue(2, 0.3f); break;
                case 5: IncreaseFireRate(0.1f); break;
                case 6: IncreaseAmount(1); break;
                case 7: IncreaseDamageValue(3, 0.3f); break;
                case 8: IncreaseDamageValue(5, 0.3f); break;
                case 9: IncreaseFireRate(0.1f); break;
                case 10: IncreaseFireRate(0.1f); break;
                default: break;
            }
        }
    }
}
