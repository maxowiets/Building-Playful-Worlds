using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoData : MonoBehaviour
{
    public FloatData data;
    public TextMeshProUGUI ammoText;

    public void IncreaseAmmo(int clipAmount)
    {
        data.CurrentAmmo += clipAmount * data.MaxClipSize;
        if (data.CurrentAmmo > data.MaxAmmo)
        {
            data.CurrentAmmo = data.MaxAmmo;
        }
    }

    public float GetCurrentAmmo()
    {
        return data.CurrentAmmo;
    }
}
