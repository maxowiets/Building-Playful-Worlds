using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUpSpawner : MonoBehaviour
{
    public GameObject ammoPickUp;
    GameObject ammoPickUpSlot;

    public float ammoPickUpTimer;
    public float ammoPickUpTime;

    void Update()
    {
        if (ammoPickUpSlot == null)
        {
            ammoPickUpTime += Time.deltaTime;
            if (ammoPickUpTime >= ammoPickUpTimer)
            {
                ammoPickUpTime = 0;
                ammoPickUpSlot = Instantiate(ammoPickUp, transform.position, Quaternion.identity);
            }
        }
    }
}
