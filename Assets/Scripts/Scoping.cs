using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoping : MonoBehaviour
{
    public Transform weaponTransform;
    public Transform scopeTransform;
    public bool isScoping;
    public float scopeSpeed;

    private void Update()
    {
        if (isScoping)
        {
            weaponTransform.localPosition = Vector3.MoveTowards(weaponTransform.localPosition, scopeTransform.localPosition, scopeSpeed * Time.deltaTime);
        }
        else
        {
            weaponTransform.localPosition = Vector3.MoveTowards(weaponTransform.localPosition, Vector3.zero, scopeSpeed * Time.deltaTime);
        }
        isScoping = false;
    }
}
