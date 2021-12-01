using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public RaycastHit hit;
    public List<ParticleSystem> particles = new List<ParticleSystem>();
    [HideInInspector]
    public float length;
    [HideInInspector]
    public float laserCharge;
    public float damage;

    private void Start()
    {
        damage *= laserCharge;
        if (hit.collider != null)
        {
            length = Vector3.Distance(transform.position, hit.point);
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage * PlayerStats.DamageMultiplier);
            }
        }
        for (int i = 0; i < particles.Count; i++)
        {
            var ps = particles[i].shape;
            ps.radius = length * 0.5f;
            ps.position = Vector3.forward * length * 0.5f;
            var pe = particles[i].emission;
            if (i == 0)
            {
                pe.SetBurst(0, new ParticleSystem.Burst(0, (int)length * 100));
            }
            else
            {
                pe.SetBurst(0, new ParticleSystem.Burst(0, (int)length * 3));
            }
        }
        Destroy(this.gameObject, 1f);
    }
}
