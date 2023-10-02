

public class BomberPowerup : Powerup
{
    protected override void ActivatePowerup(PlayerBase player)
    {
        player.GetComponent<PlayerAttack>().Weapons[1].IncreaseLevel();
    }
}
