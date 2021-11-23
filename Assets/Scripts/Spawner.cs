using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    float timer;
    public GameObject block;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= Time.time)
        {
            timer = Time.time + 1;
            Instantiate(block, new Vector3(Random.Range(-20, 20), Random.Range(0, 5), Random.Range(-20, 20)), Quaternion.identity);
        }
    }
}
