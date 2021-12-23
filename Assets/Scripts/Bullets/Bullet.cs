using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseBulletTransform
{
    public GameObject bloodParticle;
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
        if (hit.collider.GetComponent<MaterialTypeObject>())
        {
            switch (hit.collider.GetComponent<MaterialTypeObject>().type)
            {
                case MaterialType.BLOCK:
                    var newParticle = Instantiate(hitParticle, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    Destroy(newParticle, particleDuration);
                    break;
                case MaterialType.FLESH:
                    var newBloodParticle = Instantiate(bloodParticle, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    Destroy(newBloodParticle, particleDuration);
                    break;
            }
        }

        if (hit.collider.GetComponent(typeof(IDamagable)))
        {
            hit.collider.GetComponent<IDamagable>().TakeDamage(bulletDamage);
        }

        Destroy(bullet.gameObject);
    }
}
