using UnityEngine;

public class ObstacleBlock : MonoBehaviour, IDamagable
{
    public float health;
    public float Health { get; set; }
    public MeshRenderer meshRen;

    private void Awake()
    {
        Health = health; 
        UpdateTexture();
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
        UpdateTexture();
        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    
    void UpdateTexture()
    {
        meshRen.material.SetFloat("DamageAmount", Health / health);
    }
}
