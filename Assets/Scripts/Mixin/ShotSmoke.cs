using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSmoke : MixinBase
{
    public ParticleSystem ps;
    public Material smokeMaterial;

    void Start()
    {
        ps.GetComponent<ParticleSystemRenderer>().material = smokeMaterial;
    }

    public override void Action()
    {
        ps.Play();
    }
}
