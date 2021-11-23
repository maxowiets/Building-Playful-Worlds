using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public KeyCode fire1;
    public KeyCode reload;
    public WeaponList weaponList;

    private void Update()
    {
        if (Input.GetKey(fire1))
        {
            weaponList.currentWeaponMixins.CallMixinAction();
        }
        if (Input.GetKey(reload))
        {
            weaponList.currentWeaponReloadMixin?.Action();
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            weaponList.SwitchToNextWeapon();
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            weaponList.SwitchToPreviousWeapon();
        }
    }
}
