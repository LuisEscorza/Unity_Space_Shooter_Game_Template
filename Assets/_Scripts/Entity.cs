using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    #region Fields
    [field: Header("Health Stats")]
    [field: SerializeField] private int _baseHealth;
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }

    [field: Header("Movement Stats")]
    [field: SerializeField] private float _baseMovementSpeed;
    public float MovementSpeed { get; private set; }
    #endregion

    #region Misc. Methods
    public virtual void Awake()
    {
        Health = _baseHealth;
        MaxHealth = _baseHealth;
        MovementSpeed = _baseMovementSpeed;
    }
    #endregion

    #region Health Methods
    internal void IncreaseHealth()
    {
        Health++;
    }
   
    public virtual void DecreaseHealth(int value)
    {
        Health -= value;
    }
   
    public abstract void TakeDamage(int damageValue);
   
    public abstract void Die();
  
    public void ResetHealth()
    {
        Health = _baseHealth;
    }
    #endregion

}
