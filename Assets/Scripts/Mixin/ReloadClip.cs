using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadClip : MixinBase
{
    public FloatData clipData;
    public float clipSize;

    public float reloadTimer;
    float reloadTime;
    bool isReloading;

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
            if (reloadTime >= reloadTimer)
            {
                clipData.SetData(clipSize);
                isReloading = false;
            }
        }
    }
}