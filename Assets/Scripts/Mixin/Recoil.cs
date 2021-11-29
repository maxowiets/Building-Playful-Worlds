using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MixinBase
{
    public float recoilStrength;
    public float recoilDuration;
    public float recoilRecoveryAmount;

    public PlayerCamera camLogic;
    public Transform player;

    public override void Action()
    {
        StartCoroutine(RecoilSmoother());
    }

    private void Update()
    {
        //moving camera back to original position
        camLogic.xOffset *= Mathf.Pow(0.5f, recoilRecoveryAmount * Time.deltaTime);
        camLogic.yOffset *= Mathf.Pow(0.5f, recoilRecoveryAmount * Time.deltaTime);
    }

    IEnumerator RecoilSmoother()
    {
        var randX = Random.Range(0.8f, 1f);
        //calculation to have less sideways recoil when shooting with more recoil
        var randY = Random.Range(-0.5f + (recoilStrength * 0.005f), 0.5f - (recoilStrength * 0.005f));
        for (float i = 0; i < recoilDuration; i += Time.deltaTime)
        {
            //extra smoothness in the end of the recoil
            var extraSmoother = Mathf.Clamp((1 - i * 3), 0f, 1f);
            camLogic.xOffset -= recoilStrength * 5 * randX * extraSmoother * Time.deltaTime * PlayerStats.RecoilMultiplier;
            camLogic.yOffset += recoilStrength * 5 * randY * extraSmoother * Time.deltaTime * PlayerStats.RecoilMultiplier;

            yield return 0;
        }
    }
}
