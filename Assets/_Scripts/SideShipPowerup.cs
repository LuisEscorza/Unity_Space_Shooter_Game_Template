
public class SideShipPowerup : Powerup
{
    protected override void ActivatePowerup(PlayerBase player)
    {
        player.GetComponent<PlayerAttack>().Weapons[2].IncreaseLevel();
    }
}
