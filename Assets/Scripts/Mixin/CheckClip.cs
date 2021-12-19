using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        data.CurrentAmmo -= data.AmmoCost;
        if (data.CurrentClipSize < 0)
        {
            data.CurrentAmmo += Mathf.Abs(data.CurrentClipSize);
            data.CurrentClipSize += Mathf.Abs(data.CurrentClipSize);
        }
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        UIManager.Instance.ammoCounterUI.UpdateAmmoText(data.CurrentClipSize + " / " + (data.CurrentAmmo - data.CurrentClipSize));
    }
}
