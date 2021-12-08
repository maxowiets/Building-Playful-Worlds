using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReloadClip : MixinBase
{
    public FloatData data;

    public float reloadTime;
    public bool isReloading;

    public TextMeshProUGUI ammoText;

    public override bool Check()
    {
        return !isReloading;
    }

    public override void Action()
    {
        if (data.CurrentAmmo > 0)
        {
            isReloading = true;
            reloadTime = 0;
        }
    }

    private void Update()
    {
        if (isReloading)
        {
            reloadTime += Time.deltaTime;
            if (reloadTime >= data.ReloadDuration * PlayerStats.ReloadSpeedMultiplier)
            {
                data.CurrentClipSize = data.MaxClipSize > data.CurrentAmmo ? data.CurrentAmmo : data.MaxClipSize;
                isReloading = false;
                UpdateAmmoUI();
            }
        }
    }

    public void UpdateAmmoUI()
    {
        ammoText.text = data.CurrentClipSize + " / " + (data.CurrentAmmo - data.CurrentClipSize);
    }
}