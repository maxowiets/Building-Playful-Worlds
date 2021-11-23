using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
                Debug.Log(hit.collider);
                if (hit.collider != null)
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
