using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTransform : MonoBehaviour
{
    public float speed;
    Vector3 prevPos;

    private void Update()
    {
        prevPos = transform.position;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        RaycastHit[] hits = Physics.RaycastAll(transform.position, (transform.position - prevPos).normalized, (transform.position - prevPos).magnitude);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.tag == "Enemy")
            {
                Destroy(hits[i].collider.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
