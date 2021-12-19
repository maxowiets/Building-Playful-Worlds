using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    public int clipAmount;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerControls>();
        if (player != null)
        {
            for (int i = 0; i < player.weaponList.weapons.Count; i++)
            {
                player.weaponList.weapons[i].GetComponentInChildren<AmmoData>().IncreaseAmmo(clipAmount);
            }
            player.weaponList.currentWeaponReloadMixin.GetComponent<ReloadClip>().UpdateAmmoUI();
            Destroy(this.gameObject);
        }
    }
}
