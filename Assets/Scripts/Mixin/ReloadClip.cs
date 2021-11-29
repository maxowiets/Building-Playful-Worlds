using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadClip : MixinBase
{
    public FloatData data;
    public float clipSize;

    public float reloadTime;
    public bool isReloading;

    public override bool Check()
    {
        return !isReloading;
    }

    public override void Action()
    {
        isReloading = true;
        reloadTime = 0;
    }

    private void Update()
    {
        if (isReloading)
        {
            reloadTime += Time.deltaTime;
            if (reloadTime >= data.ReloadDuration * PlayerStats.ReloadSpeedMultiplier)
            {
                data.CurrentClipSize = data.MaxClipSize;
                isReloading = false;
            }
        }
    }
}