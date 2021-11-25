using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeWeapon : MixinActionable
{
    float maxCharge = 1;
    public float currentCharge = 0;
    public float chargeSpeed;
    public bool charging;

    public Transform firePosition;
    GameObject chargedBullet;

    public override bool Check()
    {
        return !charging;
    }

    public override void Action()
    {
        currentCharge += chargeSpeed * Time.deltaTime;
        charging = true;
    }

    private void Update()
    {
        if (currentCharge >= maxCharge || currentCharge > 0 && !charging)
        {
            currentCharge = 0;
            var newChargedBullet = Instantiate(chargedBullet, firePosition.position, Quaternion.Euler(firePosition.forward));
            newChargedBullet.transform.localScale = Vector3.one * currentCharge;
        }
        charging = false;
    }
}
