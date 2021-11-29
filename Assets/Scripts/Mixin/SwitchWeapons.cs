using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapons : MixinBase
{
    public FloatData data;
    float switchTime;
    bool isSwitching = true;

    public override bool Check()
    {
        return !isSwitching;
    }

    public override void Action()
    {
        isSwitching = true;
        switchTime = -data.SwitchWeaponDuration;
    }

    private void Update()
    {
        if (isSwitching)
        {
            switchTime += Time.deltaTime;
            if (switchTime > data.SwitchWeaponDuration)
            {
                isSwitching = false;
            }
        }
    }
}
