using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseBulletTransform
{
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
        var newParticle = Instantiate(hitParticle, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));

        if (hit.collider.GetComponent(typeof(IDamagable)))
        {
            hit.collider.GetComponent<IDamagable>().TakeDamage(bulletDamage);
        }

        Destroy(newParticle, particleDuration);
        Destroy(bullet.gameObject);
    }
}
