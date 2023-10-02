
public class MedkitPowerup : Powerup
{
    protected override void ActivatePowerup(PlayerBase player)
    {
        player.HealUp();
    }
}
