using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckClip : MixinActionable
{
    public FloatData clipData;
    public float ammoPerShot;

    public override bool Check()
    {
        if (clipData.GetData() <= 0)
        {
            if (actionMixin.Check())
            {
                actionMixin.Action();
            }
            return false;
        }
        return true;
    }

    public override void Action()
    {
        clipData.AddIncrement(-ammoPerShot);
        if (clipData.GetData() < 0)
        {
            clipData.SetData(0);
        }
    }
}
