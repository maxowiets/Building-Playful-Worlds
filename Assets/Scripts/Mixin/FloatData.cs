using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatData : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField]
    float damage;
    public float Damage { get; set; }

    [SerializeField]
    float shotsPerSecond;
    public float ShotsPerSecond { get; set; }

    [SerializeField]
    float bulletsPerShot;
    public float BulletsPerShot { get; set; }

    [SerializeField]
    float accuracy;
    public float Accuracy { get; set; }

    [SerializeField]
    float recoilStrength;
    public float RecoilStrength { get; set; }

    [SerializeField]
    float recoilDuration;
    public float RecoilDuration { get; set; }

    [SerializeField]
    float recoilRecoveryStrength;
    public float RecoilRecoveryStrength { get; set; }

    [Header("Clip and Ammo")]
    [SerializeField]
    float currentClipSize;
    public float CurrentClipSize { get; set; }

    [SerializeField]
    float maxClipSize;
    public float MaxClipSize { get; set; }

    [SerializeField]
    float currentAmmo;
    public float CurrentAmmo { get; set; }

    [SerializeField]
    float maxAmmo;
    public float MaxAmmo { get; set; }

    [SerializeField]
    float ammoCost;
    public float AmmoCost { get; set; }

    [Header("Etc.")]
    [SerializeField]
    float reloadDuration;
    public float ReloadDuration { get; set; }

    [SerializeField]
    float switchWeaponDuration;
    public float SwitchWeaponDuration { get; set; }

    [SerializeField]
    float scopeSpeed;
    public float ScopeSpeed { get; set; }

    private void Awake()
    {
        Damage = damage;
        ShotsPerSecond = shotsPerSecond;
        BulletsPerShot = bulletsPerShot;
        Accuracy = accuracy;
        RecoilStrength = recoilStrength;
        RecoilDuration = recoilDuration;
        RecoilRecoveryStrength = recoilRecoveryStrength;
        CurrentClipSize = currentClipSize;
        MaxClipSize = maxClipSize;
        CurrentAmmo = currentAmmo;
        MaxAmmo = maxAmmo;
        AmmoCost = ammoCost;
        ReloadDuration = reloadDuration;
        SwitchWeaponDuration = switchWeaponDuration;
        ScopeSpeed = scopeSpeed;
    }
}
