using System.Collections;
using UnityEngine;

public class EnemyBlaster : Weapon
{

    public override IEnumerator WeaponFireAction()
    {

        yield return new WaitForSeconds(1);
        while (true)
        {
            if (AttackTimer >= FireRate)
            {
                Projectile newProjectile = ObjectPoolManager.Instance.SpawnObject(_projectilePrefab, _projectileSpawnPoints[0].position, _projectileSpawnPoints[0].rotation, ObjectPoolManager.PoolType.EnemyProjectile).GetComponent<Projectile>();
                PassProjectileStats(newProjectile);
                AudioManager.Instance.PlaySFX(FireAudio);
                ResetAttackTimer();
            }
            yield return null;
        }
    }
}



