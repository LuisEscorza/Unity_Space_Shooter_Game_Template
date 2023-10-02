
public class BlasterPowerup : Powerup
{
    protected override void ActivatePowerup(PlayerBase player)
    {
        player.GetComponent<PlayerAttack>().Weapons[0].IncreaseLevel();
    }
}
