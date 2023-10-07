
public class MedkitPickup : Pickup
{
    protected override void ActivatePowerup(PlayerBase player)
    {
        player.HealUp();
    }
}
