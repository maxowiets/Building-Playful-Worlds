using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponList : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();
    public int currentWeaponNumber;

    public CallMixins currentWeaponMixins;
    public MixinBase currentWeaponReloadMixin;
    public MixinBase currentWeaponSwitching;
    public ShootingMode currentWeaponShootingMode;

    private void Awake()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            weapons.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }

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
                    currentWeaponReloadMixin.GetComponent<ReloadClip>().Invoke("UpdateAmmoUI", Time.deltaTime);
                }
                else
                {
                    weapons[i].SetActive(false);
                }
            }
        }
    }

    public IEnumerator NewSwitchWeapon(int switchTo)
    {
        if (currentWeaponNumber != switchTo)
        {
            //stop reloading if weapon is reloading
            if (currentWeaponReloadMixin != null)
            {
                if (currentWeaponReloadMixin.GetComponent<ReloadClip>().isReloading)
                {
                    currentWeaponReloadMixin.GetComponent<ReloadClip>().isReloading = false;
                    currentWeaponReloadMixin.GetComponent<ReloadClip>().reloadTime = 0;
                }
            }
            currentWeaponSwitching.Action();

            //start switching sequence
            yield return new WaitForSeconds(currentWeaponSwitching.GetComponent<SwitchWeapons>().data.SwitchWeaponDuration);
            weapons[currentWeaponNumber].SetActive(false);
            currentWeaponNumber = switchTo;

            if (currentWeaponNumber > weapons.Count - 1) currentWeaponNumber = 0;
            if (currentWeaponNumber < 0) currentWeaponNumber = weapons.Count - 1;

            weapons[currentWeaponNumber].SetActive(true);
            NewCurrentWeapon();
            yield return 0;
            currentWeaponReloadMixin.GetComponent<ReloadClip>().UpdateAmmoUI();
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
