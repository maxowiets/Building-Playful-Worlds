using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilAnimation : MixinBase
{
    public Animator anim;

    public override void Action()
    {
        anim.SetTrigger("Recoil");
    }
}
