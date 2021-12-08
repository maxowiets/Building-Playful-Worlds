using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public KeyCode fire1;
    public KeyCode scope;
    public KeyCode reload;
    List<KeyCode> weaponKeyCodes = new List<KeyCode>();
    public WeaponList weaponList;

    private void Start()
    {
        for (int i = 0; i < weaponList.weapons.Count; i++)
        {
            weaponKeyCodes.Add((KeyCode)i + 49);
        }
    }

    private void Update()
    {
        //---------------------------------------- Shooting -------------------------------------------\\
        switch (weaponList.currentWeaponShootingMode?.shootingMode)
        {
            case ShootingModeEnum.SEMI:
                if (Input.GetKeyDown(fire1))
                {
                    weaponList.currentWeaponMixins.CallMixinAction();
                }
                break;
            case ShootingModeEnum.AUTO:
                if (Input.GetKey(fire1))
                {
                    weaponList.currentWeaponMixins.CallMixinAction();
                }
                break;
            default:
                break;
        }
        //---------------------------------------------------------------------------------------------\\

        //--------------------------------------- Reloading -------------------------------------------\\
        if (Input.GetKeyDown(reload))
        {
            weaponList.currentWeaponReloadMixin?.Action();
        }
        //---------------------------------------------------------------------------------------------\\

        //--------------------------------------- Switching weapons -----------------------------------\\
        if (Input.mouseScrollDelta.y > 0)
        {
            if (weaponList.currentWeaponSwitching.Check())
            {
                StartCoroutine(weaponList.NewSwitchWeapon(weaponList.currentWeaponNumber + 1));
            }
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            if (weaponList.currentWeaponSwitching.Check())
            {
                StartCoroutine(weaponList.NewSwitchWeapon(weaponList.currentWeaponNumber - 1));
            }
        }
        for (int i = 0; i < weaponList.weapons.Count; i++)
        {
            if (Input.GetKey(weaponKeyCodes[i]))
            {
                if (weaponList.currentWeaponSwitching.Check())
                {
                    StartCoroutine(weaponList.NewSwitchWeapon(i));
                }
            }
        }
        //---------------------------------------------------------------------------------------------\\

        //-------------------------------------- Scoping ----------------------------------------------\\
        if (Input.GetKey(scope))
        {
            if (weaponList.currentWeaponMixins.GetComponent<Scoping>() && weaponList.currentWeaponSwitching.Check())
            {
                weaponList.currentWeaponMixins.GetComponent<Scoping>().isScoping = true;
            }
        }
        //---------------------------------------------------------------------------------------------\\
    }
}
