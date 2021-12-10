using UnityEngine;

public class ObstacleBlock : MonoBehaviour, IDamagable
{
    public float health;
    public float Health { get; set; }

    private void Awake()
    {
        Health = health;
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    
}
