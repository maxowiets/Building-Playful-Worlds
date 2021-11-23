using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRaycast : MixinBase
{
    public Transform firePosition;
    public float range;
    public float accuracy;
    public LayerMask layerMask;

    public override void Action()
    {
        RaycastHit hit;

        Vector3 shootDirection = firePosition.transform.forward;
        var accuracyCalculation = (100f - accuracy) / 1000f;
        //var accuracyCalculation = (100f - (100f - (100f - accuracy) * scopeAccuracyMultiplier)) / 1000f;
        Vector3 accuracyOffset = new Vector3(Random.Range(-accuracyCalculation, accuracyCalculation), Random.Range(-accuracyCalculation, accuracyCalculation));
        shootDirection = shootDirection + firePosition.TransformDirection(accuracyOffset);

        if (Physics.Raycast(firePosition.position, shootDirection, out hit, range, layerMask))
        {
            if (hit.collider != null)
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
