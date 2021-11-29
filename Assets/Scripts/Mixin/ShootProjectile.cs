using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MixinBase
{
    public FloatData data;
    public GameObject bullet;
    public Transform firePosition;

    public override void Action()
    {
        for (int i = 0; i < data.BulletsPerShot; i++)
        {
            Vector3 shootDirection = firePosition.transform.forward;
            var accuracyCalculation = ((100f - data.Accuracy) / 1000f) * PlayerStats.AccuracyMultiplier;
            Vector3 accuracyOffset = new Vector3(Random.Range(-accuracyCalculation, accuracyCalculation), Random.Range(-accuracyCalculation, accuracyCalculation));
            shootDirection = shootDirection + firePosition.TransformDirection(accuracyOffset);
            Quaternion bulletRotation = Quaternion.LookRotation(shootDirection);

            Instantiate(bullet, firePosition.position, bulletRotation);
        }
    }
}