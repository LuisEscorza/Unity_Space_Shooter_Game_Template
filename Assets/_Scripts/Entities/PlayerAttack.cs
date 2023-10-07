using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    #region Fields
    [field: Header("Base Variables")]
    [field: SerializeField] public Weapon[] Weapons { get; private set; }
    #endregion


    public void UseWeapon(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            foreach (Weapon weapon in Weapons)
                if (weapon.isActiveAndEnabled)
                    weapon.StartWeaponAction();
        }
        else if (ctx.canceled)
            foreach (Weapon weapon in Weapons)
                weapon.StopWeaponAction(); 
    }
}
