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

    private void Start()
    {
        Destroy(bullet.gameObject ,bulletLifeTime);
    }

    private void Update()
    {
        prevPos = bullet.transform.position;
        bullet.transform.Translate(Vector3.forward * speed * Time.deltaTime);

        RaycastHit hit; 
        Physics.Raycast(bullet.transform.position, (bullet.transform.position - prevPos).normalized, out hit, (bullet.transform.position - prevPos).magnitude);

        if (hit.collider != null)
        {
            var hitParticle = Instantiate(hitParticles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            Destroy(hitParticle.gameObject, particleDuration);
            Destroy(bullet.gameObject);

            if (hit.collider?.gameObject.tag == "Enemy")
            {
                Destroy(hit.collider.gameObject);
            }
        }


        //RaycastHit[] hits = Physics.RaycastAll(bullet.transform.position, (bullet.transform.position - prevPos).normalized, (bullet.transform.position - prevPos).magnitude);

        //for (int i = 0; i < hits.Length; i++)
        //{
        //    if (hits[i].collider.gameObject.tag == "Enemy")
        //    {
        //        Destroy(hits[i].collider.gameObject);
        //        Destroy(this.gameObject);
        //    }
        //}
    }
}
