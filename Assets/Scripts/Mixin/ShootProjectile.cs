using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MixinBase
{
    public GameObject bullet;
    public Transform firePosition;
    public float shotsPerShot;
    public float accuracy;

    public override void Action()
    {
        for (int i = 0; i < shotsPerShot; i++)
        {
            Vector3 shootDirection = firePosition.transform.forward;
            var accuracyCalculation = ((100f - accuracy) / 1000f) * PlayerStats.AccuracyMultiplier;
            Vector3 accuracyOffset = new Vector3(Random.Range(-accuracyCalculation, accuracyCalculation), Random.Range(-accuracyCalculation, accuracyCalculation));
            shootDirection = shootDirection + firePosition.TransformDirection(accuracyOffset);
            Quaternion bulletRotation = Quaternion.LookRotation(shootDirection);

            Instantiate(bullet, firePosition.position, bulletRotation);
        }
    }
}