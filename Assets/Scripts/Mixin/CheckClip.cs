using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckClip : MixinActionable
{
    public FloatData data;

    public override bool Check()
    {
        if (data.CurrentClipSize <= 0)
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
        data.CurrentClipSize -= data.AmmoCost;
        if (data.CurrentClipSize < 0)
        {
            data.CurrentClipSize = 0;
        }
    }
}
