using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTransform : MonoBehaviour
{
    public float speed;
    Vector3 prevPos;
    public Transform bullet;
    public GameObject hitParticles;
    public float particleDuration;
    public float bulletLifeTime;
    public float bulletDamage;

    public virtual void Start()
    {
        Destroy(bullet.gameObject ,bulletLifeTime);
    }

    public virtual void Update()
    {
        prevPos = bullet.transform.position;
        bullet.transform.Translate(Vector3.forward * speed * Time.deltaTime);

        RaycastHit hit;
        Physics.SphereCast(bullet.transform.position, bullet.localScale.x * 0.5f, (bullet.transform.position - prevPos).normalized, out hit, (bullet.transform.position - prevPos).magnitude);

        if (hit.collider != null)
        {
            var hitParticle = Instantiate(hitParticles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));

            //if (hit.collider.gameObject.tag == "Enemy")
            //{
            //    Destroy(hit.collider.gameObject);
            //}

            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(bulletDamage * PlayerStats.DamageMultiplier);
            }

            Destroy(hitParticle.gameObject, particleDuration);
            Destroy(bullet.gameObject);
        }
    }
}
