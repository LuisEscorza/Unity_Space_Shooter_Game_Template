using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideShip : Weapon
{
    #region Fields
    [field: Header("Sideship Settings")]
    [field: SerializeField] public GameObject SideShipPrefab { get; private set; }
    [field: SerializeField] public Transform[] ShipSpawnPoints { get; private set; }
    private readonly List<Transform> _sideShips = new();
    #endregion


    private void OnEnable()
    {
        var newShip = Instantiate(SideShipPrefab, ShipSpawnPoints[1].position, ShipSpawnPoints[1].rotation);
        newShip.transform.SetParent(this.transform);
        _sideShips.Add(newShip.transform);
    }

    public override IEnumerator WeaponFireAction()
    {
        while (true)
        {
            if (AttackTimer >= FireRate)
            {
                Projectile newProjectile;
                foreach (Transform shipInstance in _sideShips)
                {
                    Vector2 projectileSpawnPosition = new(shipInstance.position.x, shipInstance.position.y + 40);
                    newProjectile = ObjectPoolManager.Instance.SpawnObject(_projectilePrefab, projectileSpawnPosition, shipInstance.rotation, ObjectPoolManager.PoolType.PlayerProjectile).GetComponent<Projectile>();
                    PassProjectileStats(newProjectile);
                }
                AudioManager.Instance.PlaySFX(FireAudio, 1f, 1.5f);
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
            GameObject newShip;
            switch (Level)
            {
                case 2: IncreaseDamageValue(1, 0.2f); break;
                case 3: IncreaseDamageValue(2, 0.2f); break;
                case 4:
                    newShip = Instantiate(SideShipPrefab, ShipSpawnPoints[2].position, ShipSpawnPoints[2].rotation);
                    newShip.transform.SetParent(this.transform);
                    _sideShips.Add(newShip.transform); break;
                case 5: IncreaseFireRate(0.4f); break;
                case 6: IncreaseFireRate(0.4f); break;
                case 7:
                    newShip = Instantiate(SideShipPrefab, ShipSpawnPoints[0].position, ShipSpawnPoints[0].rotation);
                    newShip.transform.SetParent(this.transform);
                    _sideShips.Add(newShip.transform); break;
                case 8:
                    newShip = Instantiate(SideShipPrefab, ShipSpawnPoints[3].position, ShipSpawnPoints[3].rotation);
                    newShip.transform.SetParent(this.transform);
                    _sideShips.Add(newShip.transform); break;
                case 9: IncreaseFireRate(0.3f); break;
                case 10: IncreaseDamageValue(3, 0.2f); break;
            }
        }
    }


}
