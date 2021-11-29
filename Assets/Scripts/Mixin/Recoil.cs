using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MixinBase
{
    public FloatData data;

    public PlayerCamera camLogic;
    public Transform player;

    public override void Action()
    {
        StartCoroutine(RecoilSmoother());
    }

    private void Update()
    {
        //moving camera back to original position
        camLogic.xOffset *= Mathf.Pow(0.5f, data.RecoilRecoveryStrength * Time.deltaTime);
        camLogic.yOffset *= Mathf.Pow(0.5f, data.RecoilRecoveryStrength * Time.deltaTime);
    }

    IEnumerator RecoilSmoother()
    {
        var randX = Random.Range(0.8f, 1f);
        //calculation to have less sideways recoil when shooting with more recoil
        var randY = Random.Range(-0.5f + (data.RecoilStrength * 0.005f), 0.5f - (data.RecoilStrength * 0.005f));
        for (float i = 0; i < data.RecoilDuration; i += Time.deltaTime)
        {
            //extra smoothness in the end of the recoil
            var extraSmoother = Mathf.Clamp((1 - i * 3), 0f, 1f);
            camLogic.xOffset -= data.RecoilStrength * 5 * randX * extraSmoother * Time.deltaTime * PlayerStats.RecoilMultiplier;
            camLogic.yOffset += data.RecoilStrength * 5 * randY * extraSmoother * Time.deltaTime * PlayerStats.RecoilMultiplier;

            yield return 0;
        }
    }
}
