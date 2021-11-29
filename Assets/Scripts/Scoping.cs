using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoping : MonoBehaviour
{
    public FloatData data;
    public Transform weaponTransform;
    public Transform scopeTransform;
    public bool isScoping;

    private void Update()
    {
        if (isScoping)
        {
            weaponTransform.localPosition = Vector3.MoveTowards(weaponTransform.localPosition, scopeTransform.localPosition, data.ScopeSpeed * Time.deltaTime);
        }
        else
        {
            weaponTransform.localPosition = Vector3.MoveTowards(weaponTransform.localPosition, Vector3.zero, data.ScopeSpeed * Time.deltaTime);
        }
        isScoping = false;
    }
}
