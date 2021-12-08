using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpsMovement : MonoBehaviour
{
    float timer;
    public float bopSpeed;
    public float bopHeight;
    public float rotateSpeed;

    void Update()
    {
        timer += Time.deltaTime;
        transform.position += Vector3.up * Mathf.Cos(timer * bopSpeed) * bopHeight * Time.deltaTime;

        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }
}
