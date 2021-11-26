using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedBulletParticles : MonoBehaviour
{
    public Transform bulletTransform;
    public List<Transform> ps = new List<Transform>();

    private void Start()
    {
        for (int i = 0; i < ps.Count; i++)
        {
            ps[i].localScale = bulletTransform.localScale;
        }
    }
    void Update()
    {
        for (int i = 0; i < ps.Count; i++)
        {
            ps[i].localScale = bulletTransform.localScale;
        }
    }
}
