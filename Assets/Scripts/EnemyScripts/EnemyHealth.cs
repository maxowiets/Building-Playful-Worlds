using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth :  IDamagable
{
    public float Health { get; set; }

    public EnemyHealth(float health)
    {
        Health = health;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            //Die();
        }
    }

    //void Die()
    //{
    //    Destroy(this.gameObject);
    //}
}
