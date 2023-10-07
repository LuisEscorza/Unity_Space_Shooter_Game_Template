

public class BomberPickup : Pickup
{
    protected override void ActivatePowerup(PlayerBase player)
    {
        player.GetComponent<PlayerAttack>().Weapons[1].IncreaseLevel();
    }
}
