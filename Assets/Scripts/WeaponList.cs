using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponList : MonoBehaviour
{
    public List<GameObject> weapons;
    int currentWeaponNumber;

    public CallMixins currentWeaponMixins;
    public MixinBase currentWeaponReloadMixin;

    private void Start()
    {
        if (weapons.Count != 0)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                if (i == 0)
                {
                    weapons[i].SetActive(true);
                    currentWeaponNumber = i;
                    NewCurrentWeapon();
                }
                else
                {
                    weapons[i].SetActive(false);
                }
            }
        }
    }

    public void SwitchToNextWeapon()
    {
        weapons[currentWeaponNumber].SetActive(false);
        currentWeaponNumber++;
        if (currentWeaponNumber > weapons.Count - 1)
        {
            currentWeaponNumber = 0;
        }
        weapons[currentWeaponNumber].SetActive(true);
        NewCurrentWeapon();
    }

    public void SwitchToPreviousWeapon()
    {
        weapons[currentWeaponNumber].SetActive(false);
        currentWeaponNumber--;
        if (currentWeaponNumber < 0)
        {
            currentWeaponNumber = weapons.Count - 1;
        }
        weapons[currentWeaponNumber].SetActive(true);
        NewCurrentWeapon();
    }

    public void SwitchWeapon(int newWeaponNumber)
    {
        if (currentWeaponNumber != newWeaponNumber)
        {
            weapons[currentWeaponNumber].SetActive(false);
            currentWeaponNumber = newWeaponNumber;
            weapons[currentWeaponNumber].SetActive(true);
            NewCurrentWeapon();
        }
    }

    void NewCurrentWeapon()
    {
        currentWeaponMixins = weapons[currentWeaponNumber].GetComponentInChildren<CallMixins>();
        currentWeaponReloadMixin = currentWeaponMixins.GetComponentInChildren<ReloadClip>();
    }
}
