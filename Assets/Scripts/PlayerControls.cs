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
        if (Input.GetKey(fire1))
        {
            weaponList.currentWeaponMixins.CallMixinAction();
        }
        if (Input.GetKey(reload))
        {
            weaponList.currentWeaponReloadMixin?.Action();
        }

        //Switching weapons
        if (Input.mouseScrollDelta.y > 0)
        {
            if (weaponList.currentWeaponSwitching.Check())
            {
                if (weaponList.currentWeaponReloadMixin != null)
                {
                    if (weaponList.currentWeaponReloadMixin.GetComponent<ReloadClip>().isReloading)
                    {
                        weaponList.currentWeaponReloadMixin.GetComponent<ReloadClip>().isReloading = false;
                        weaponList.currentWeaponReloadMixin.GetComponent<ReloadClip>().reloadTime = 0;
                    }
                }
                weaponList.currentWeaponSwitching.Action();
                StartCoroutine(weaponList.SwitchToNextWeapon());
            }
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            if (weaponList.currentWeaponSwitching.Check())
            {
                if (weaponList.currentWeaponReloadMixin != null)
                {
                    if (weaponList.currentWeaponReloadMixin.GetComponent<ReloadClip>().isReloading)
                    {
                        weaponList.currentWeaponReloadMixin.GetComponent<ReloadClip>().isReloading = false;
                        weaponList.currentWeaponReloadMixin.GetComponent<ReloadClip>().reloadTime = 0;
                    }
                }
                weaponList.currentWeaponSwitching.Action();
                StartCoroutine(weaponList.SwitchToPreviousWeapon());
            }
        }
        for (int i = 0; i < weaponList.weapons.Count; i++)
        {
            if (weaponList.currentWeaponSwitching.Check())
            {
                if (Input.GetKey(weaponKeyCodes[i]))
                {
                    weaponList.currentWeaponSwitching.Action();
                    StartCoroutine(weaponList.SwitchWeapon(i));
                }
            }
        }

        if (Input.GetKey(scope))
        {
            if (weaponList.currentWeaponMixins.GetComponent<Scoping>() && weaponList.currentWeaponSwitching.Check())
            {
                weaponList.currentWeaponMixins.GetComponent<Scoping>().isScoping = true;
            }
        }
    }
}
