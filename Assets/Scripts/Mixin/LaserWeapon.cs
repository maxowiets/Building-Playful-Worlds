using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : MixinBase
{
    public Transform firePosition;
    float maxRange = 100;
    public GameObject laser;

    public float maxCharge;
    public float currentCharge = 0;
    public float chargeSpeed;
    public RectTransform chargeBar;
    float chargeBarStartSize;

    private void Start()
    {
        chargeBarStartSize = chargeBar.sizeDelta.x;
    }

    public override void Action()
    {
        RaycastHit hit;

        var newLaser = Instantiate(laser, firePosition.transform.position, Quaternion.LookRotation(firePosition.transform.forward)).GetComponent<Laser>();
        newLaser.length = maxRange;
        newLaser.laserCharge = currentCharge / maxCharge;

        if (Physics.Raycast(firePosition.position, firePosition.transform.forward, out hit, maxRange))
        {
            newLaser.hit = hit;
        }

        currentCharge = 0;
    }

    private void Update()
    {
        if (currentCharge < maxCharge)
        {
            currentCharge += chargeSpeed * PlayerStats.ChargeSpeedMultiplier * Time.deltaTime;
            chargeBar.sizeDelta = new Vector2((currentCharge / maxCharge) * chargeBarStartSize, chargeBar.sizeDelta.y);
        }
    }
}
