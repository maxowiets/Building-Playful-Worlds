using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int totalBuffs;

    //------------------------- Weapon Stats ----------------------------\\
    public float damageMultiplier; 
    public static float DamageMultiplier { get; set; }  

    public float attackSpeedMultiplier;
    public static float AttackSpeedMultiplier { get; set; }

    public float recoilMultiplier;
    public static float RecoilMultiplier { get; set; }

    public float accuracyMultiplier;
    public static float AccuracyMultiplier { get; set; }

    public float reloadSpeedMultiplier;
    public static float ReloadSpeedMultiplier { get; set; }

    public float chargeSpeedMultiplier;
    public static float ChargeSpeedMultiplier { get; set; }

    //------------------------- Player Stats ----------------------------\\
    public float speedMultiplier;                                  
    public static float SpeedMultiplier { get; set; }              

    public float sprintSpeedMultiplier;                            
    public static float SprintSpeedMultiplier { get; set; }        

    public float jumpHeightMultiplier;                             
    public static float JumpHeightMultiplier { get; set; }         

    private void Awake()
    {
        totalBuffs = 0;
        DamageMultiplier = damageMultiplier;
        AttackSpeedMultiplier = attackSpeedMultiplier;
        RecoilMultiplier = recoilMultiplier;
        AccuracyMultiplier = accuracyMultiplier;
        ReloadSpeedMultiplier = reloadSpeedMultiplier;
        ChargeSpeedMultiplier = chargeSpeedMultiplier;
        SpeedMultiplier = speedMultiplier;
        SprintSpeedMultiplier = sprintSpeedMultiplier;
        JumpHeightMultiplier = jumpHeightMultiplier;
    }
}
