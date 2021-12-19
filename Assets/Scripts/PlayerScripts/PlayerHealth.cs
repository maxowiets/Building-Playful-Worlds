using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class PlayerHealth : MonoBehaviour, IDamagable
{
    public float health;
    public float Health { get; set; }
    public Volume ppVolume;
    Vignette vignette;
    public float vignetteDecaySpeed;

    PlayerControls playerControls;
    PlayerMovementVelocity playerMovementVelocity;
    public PlayerCamera playerCamera;


    private void Awake()
    {
        ppVolume.profile.TryGet(out vignette);
        playerControls = GetComponent<PlayerControls>();
        playerMovementVelocity = GetComponent<PlayerMovementVelocity>();
    }
    void Start()
    {
        Health = health;
        UIManager.Instance.playerHealthUI.UpdateHealthUI(health, health);
    }

    private void Update()
    {
        if (vignette.intensity.value > Mathf.Pow((health-Health)/health, 2) * 0.8f)
        { 
            vignette.intensity.value -= vignetteDecaySpeed * Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        UIManager.Instance.playerHealthUI.UpdateHealthUI(Health, health);
        vignette.intensity.value += (damage / health) * 3;

        if (Health <= 0)
        {
            playerControls.enabled = false;
            playerMovementVelocity.enabled = false;
            this.enabled = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic = true;
            playerCamera.enabled = false;
        }
    }
}
