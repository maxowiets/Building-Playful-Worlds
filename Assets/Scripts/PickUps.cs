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
        }
    }
}
