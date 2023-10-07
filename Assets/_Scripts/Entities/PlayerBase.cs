

public class PlayerBase : Entity
{

    public override void TakeDamage(int damageValue)
    {
        if (Health > 0)
        {
            DecreaseHealth(damageValue);
            GameplayManager.Manager.HUDManager.UpdateCurrentHealth(Health);
        }
        if (Health <= 0) Die();
    }


    public void HealUp()
    {
        if (Health < MaxHealth)
        {
            IncreaseHealth();
            GameplayManager.Manager.HUDManager.UpdateCurrentHealth(Health);
        }
    }

    public override void Die()
    {
        Destroy(gameObject);
        GameplayManager.Manager.GameOver();
    }

}
