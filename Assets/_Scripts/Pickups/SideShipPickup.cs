
public class SideShipPickup : Pickup
{
    protected override void ActivatePowerup(PlayerBase player)
    {
        player.GetComponent<PlayerAttack>().Weapons[2].IncreaseLevel();
    }
}
