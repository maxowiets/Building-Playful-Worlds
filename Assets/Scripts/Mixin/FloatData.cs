using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatData : MonoBehaviour
{
    [SerializeField]
    float currentClipSize;
    public float CurrentClipSize { get; set; }

    [SerializeField]
    float maxClipSize;
    public float MaxClipSize { get; set; }

    [SerializeField]
    float shotsPerSecond;
    public float ShotsPerSecond { get; set; }

    [SerializeField]
    float ammoCost;
    public float AmmoCost { get; set; }

    [SerializeField]
    float reloadDuration;
    public float ReloadDuration { get; set; }

    [SerializeField]
    float recoilStrength;
    public float RecoilStrength { get; set; }

    [SerializeField]
    float recoilDuration;
    public float RecoilDuration { get; set; }

    [SerializeField]
    float recoilRecoveryStrength;
    public float RecoilRecoveryStrength { get; set; }

    [SerializeField]
    float switchWeaponDuration;
    public float SwitchWeaponDuration { get; set; }

    [SerializeField]
    float scopeSpeed;
    public float ScopeSpeed { get; set; }

    [SerializeField]
    float bulletsPerShot;
    public float BulletsPerShot { get; set; }

    [SerializeField]
    float accuracy;
    public float Accuracy { get; set; }

    private void Start()
    {
        CurrentClipSize = currentClipSize;
        MaxClipSize = maxClipSize;
        ShotsPerSecond = shotsPerSecond;
        AmmoCost = ammoCost;
        ReloadDuration = reloadDuration;
        RecoilStrength = recoilStrength;
        RecoilDuration = recoilDuration;
        RecoilRecoveryStrength = recoilRecoveryStrength;
        SwitchWeaponDuration = switchWeaponDuration;
        ScopeSpeed = scopeSpeed;
        BulletsPerShot = bulletsPerShot;
        Accuracy = accuracy;
    }
}
