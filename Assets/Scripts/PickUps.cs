using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    public PickUpType pickUpType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IncreaseStat();
            Destroy(this.gameObject);
        }
    }

    void IncreaseStat()
    {
        switch (pickUpType)
        {
            case PickUpType.DAMAGE:
                PlayerStats.DamageMultiplier += 0.1f;
                break;
            case PickUpType.ATTACKSPEED:
                PlayerStats.AttackSpeedMultiplier += 0.1f;
                break;
            case PickUpType.RECOIL:
                PlayerStats.RecoilMultiplier *= 0.9f;
                break;
            case PickUpType.ACCURACY:
                PlayerStats.AccuracyMultiplier *= 0.9f;
                break;
            case PickUpType.RELOADSPEED:
                PlayerStats.ReloadSpeedMultiplier *= 0.9f;
                break;
            case PickUpType.CHARGESPEED:
                PlayerStats.ChargeSpeedMultiplier += 0.1f;
                break;
            case PickUpType.MOVEMENTSPEED:
                PlayerStats.SpeedMultiplier += 0.1f;
                break;
            case PickUpType.SPRINTSPEED:
                PlayerStats.SprintSpeedMultiplier += 0.1f;
                break;
            case PickUpType.JUMPHEIGHT:
                PlayerStats.JumpHeightMultiplier += 0.1f;
                break;
            default:
                break;
        }
    }
}
