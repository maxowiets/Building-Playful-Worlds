using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCooldown : MixinBase
{
    public FloatData data;
    float cooldownTime;
    bool isCool;

    public override bool Check()
    {
        return isCool;
    }

    public override void Action()
    {
        isCool = false;
        cooldownTime = 0.0f;
    }

    void Update()
    {
        if (!isCool)
        {
            cooldownTime += Time.deltaTime;
            if (cooldownTime >= 1f / (data.ShotsPerSecond * PlayerStats.AttackSpeedMultiplier))
            {
                isCool = true;
            }
        }
    }
}
