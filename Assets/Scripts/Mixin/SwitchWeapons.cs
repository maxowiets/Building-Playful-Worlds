using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapons : MixinBase
{
    public float switchTimer = 1;
    float switchTime;
    bool isSwitching = true;

    public override bool Check()
    {
        return !isSwitching;
    }

    public override void Action()
    {
        isSwitching = true;
        switchTime = -switchTimer;
    }

    private void Update()
    {
        if (isSwitching)
        {
            switchTime += Time.deltaTime;
            if (switchTime > switchTimer)
            {
                isSwitching = false;
            }
        }
    }
}
