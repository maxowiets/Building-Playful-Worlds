using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAnimation : MixinBase
{
    public FloatData data;
    public Animator anim;
    public AnimationClip clip;
    float animationSpeed;

    public override void Action()
    {
        if (1 / (data.ShotsPerSecond * PlayerStats.AttackSpeedMultiplier) < clip.length)
        {
            animationSpeed = clip.length / (1 / (data.ShotsPerSecond * PlayerStats.AttackSpeedMultiplier));
        }
        else animationSpeed = 1;
        anim.SetFloat("ShootAnimationSpeed", animationSpeed);
        anim.SetTrigger("Shoot");
    }
}
