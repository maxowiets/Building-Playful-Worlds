using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : BaseBulletTransform
{
    public float blastRadius;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        if (hit.collider != null)
        {
            OnHit();
        }
    }

    void OnHit()
    {
        Collider[] hitObjects = Physics.OverlapSphere(hit.point, blastRadius);

        foreach (var obj in hitObjects)
        {
            if (obj.GetComponent<Enemy>())
            {
                obj.GetComponent<Enemy>().TakeDamage(bulletDamage);
            }
        }
        Destroy(bullet.gameObject);
    }

    private void OnDestroy()
    {
        Vector3 explosionPosition = hit.collider != null ? hit.point : transform.position;
        var newExplosion = Instantiate(hitParticle, explosionPosition, Quaternion.identity);

        Destroy(newExplosion, particleDuration);
    }
}
