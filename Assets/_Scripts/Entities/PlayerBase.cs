

public class PlayerBase : Entity
{

    public override void TakeDamage(int damageValue)
    {
        if (Health > 0)
        {
            DecreaseHealth(damageValue);
            EventManager.Instance.TriggerOnPlayerHealthChanged(Health);
        }
        if (Health <= 0) Die();
    }


    public void HealUp()
    {
        if (Health < MaxHealth)
        {
            IncreaseHealth();
            EventManager.Instance.TriggerOnPlayerHealthChanged(Health);
        }
    }

    public override void Die()
    {
        EventManager.Instance.TriggerOnPlayerDied();
        Destroy(gameObject);
    }

}
