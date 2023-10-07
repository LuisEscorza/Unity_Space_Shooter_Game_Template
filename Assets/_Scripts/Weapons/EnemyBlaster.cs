using System.Collections;
using UnityEngine;

public class EnemyBlaster : Weapon
{

    public override IEnumerator WeaponFireAction()
    {
        while (true)
        {
            if (AttackTimer >= FireRate)
            {
                Projectile newProjectile = ObjectPoolManager.Manager.SpawnObject(_projectilePrefab, _projectileSpawnPoints[0].position, _projectileSpawnPoints[0].rotation, ObjectPoolManager.PoolType.EnemyProjectile).GetComponent<Projectile>();
                PassProjectileStats(newProjectile);
                AudioManager.Manager.PlaySFX(FireAudio);
                ResetAttackTimer();
            }
            yield return null;
        }
    }
}



