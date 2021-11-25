using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTransform : MonoBehaviour
{
    public float speed;
    Vector3 prevPos;
    public Transform rocket;
    public GameObject hitParticles;
    public float particleDuration;

    private void Update()
    {
        prevPos = rocket.transform.position;
        rocket.transform.Translate(Vector3.forward * speed * Time.deltaTime);

        RaycastHit hit;
        Physics.SphereCast(rocket.transform.position, 0.05f, (rocket.transform.position - prevPos).normalized, out hit, (rocket.transform.position - prevPos).magnitude);

        if (hit.collider != null)
        {
            var hitParticle = Instantiate(hitParticles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            Destroy(hitParticle.gameObject, particleDuration);
            Destroy(rocket.gameObject);

            if (hit.collider?.gameObject.tag == "Enemy")
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
