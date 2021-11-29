using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeWeapon : MixinBase
{
    public float maxCharge;
    public float currentCharge = 0;
    public float chargeSpeed;

    public Transform firePosition;
    public GameObject chargedBullet;
    GameObject currentChargingBullet;

    private void Start()
    {
        var newChargedBullet = Instantiate(chargedBullet, firePosition.position, Quaternion.LookRotation(firePosition.forward));
        currentChargingBullet = newChargedBullet;
        currentChargingBullet.transform.parent = firePosition;
        currentChargingBullet.transform.localScale = Vector3.one * currentCharge;
    }

    public override void Action()
    {
        currentChargingBullet.GetComponentInChildren<BulletTransform>().enabled = true;
        currentChargingBullet.transform.parent = null;
        currentCharge = 0;
        var newChargedBullet = Instantiate(chargedBullet, firePosition.position, Quaternion.LookRotation(firePosition.forward));
        currentChargingBullet = newChargedBullet;
        currentChargingBullet.transform.parent = firePosition;
    }

    private void Update()
    {
        if (currentCharge < maxCharge)
        {
            currentCharge += chargeSpeed * Time.deltaTime;
            currentChargingBullet.transform.localScale = Vector3.one * currentCharge;
        }
    }
}
