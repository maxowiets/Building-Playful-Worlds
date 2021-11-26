using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponList : MonoBehaviour
{
    public List<GameObject> weapons;
    int currentWeaponNumber;

    public CallMixins currentWeaponMixins;
    public MixinBase currentWeaponReloadMixin;
    public MixinBase currentWeaponSwitching;
    public ShootingMode currentWeaponShootingMode;

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

    public IEnumerator SwitchToNextWeapon()
    {
        yield return new WaitForSeconds(currentWeaponSwitching.GetComponent<SwitchWeapons>().switchTimer);
        weapons[currentWeaponNumber].SetActive(false);
        currentWeaponNumber++;
        if (currentWeaponNumber > weapons.Count - 1)
        {
            currentWeaponNumber = 0;
        }
        weapons[currentWeaponNumber].SetActive(true);
        NewCurrentWeapon();
    }

    public IEnumerator SwitchToPreviousWeapon()
    {
        yield return new WaitForSeconds(currentWeaponSwitching.GetComponent<SwitchWeapons>().switchTimer);
        weapons[currentWeaponNumber].SetActive(false);
        currentWeaponNumber--;
        if (currentWeaponNumber < 0)
        {
            currentWeaponNumber = weapons.Count - 1;
        }
        weapons[currentWeaponNumber].SetActive(true);
        NewCurrentWeapon();
    }

    public IEnumerator SwitchWeapon(int newWeaponNumber)
    {
        if (currentWeaponNumber != newWeaponNumber)
        {
            yield return new WaitForSeconds(currentWeaponSwitching.GetComponent<SwitchWeapons>().switchTimer);
            weapons[currentWeaponNumber].SetActive(false);
            currentWeaponNumber = newWeaponNumber;
            currentWeaponNumber = newWeaponNumber;
            weapons[currentWeaponNumber].SetActive(true);
            NewCurrentWeapon();
        }
    }

    void NewCurrentWeapon()
    {
        currentWeaponMixins = weapons[currentWeaponNumber].GetComponentInChildren<CallMixins>();
        currentWeaponReloadMixin = currentWeaponMixins.GetComponentInChildren<ReloadClip>();
        currentWeaponSwitching = currentWeaponMixins.GetComponentInChildren<SwitchWeapons>();
        currentWeaponShootingMode = currentWeaponMixins.GetComponentInChildren<ShootingMode>();
    }
}
