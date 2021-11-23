using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MixinBase
{
    public ParticleSystem ps;
    public Material muzzleFlashMaterial;

    void Start()
    {
        ps.GetComponent<ParticleSystemRenderer>().material = muzzleFlashMaterial;
    }

    public override void Action()
    {
        ps.Play();
    }
}
