using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBulletTransform : MonoBehaviour
{
    public float speed;
    Vector3 prevPos;
    public Transform bullet;
    public float bulletLifeTime;
    public float bulletDamage;

    public RaycastHit hit;

    public GameObject hitParticle;
    public float particleDuration;

    public virtual void Start()
    {
        Destroy(bullet.gameObject ,bulletLifeTime);
    }

    public virtual void Update()
    {
        prevPos = bullet.transform.position;
        bullet.transform.Translate(Vector3.forward * speed * Time.deltaTime);

        Physics.SphereCast(bullet.transform.position, bullet.localScale.x * 0.5f, (bullet.transform.position - prevPos).normalized, out hit, (bullet.transform.position - prevPos).magnitude);
    }
}
