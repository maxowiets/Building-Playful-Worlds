using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    public RectTransform healthBar;
    float healthBarStartSize;

    public TextMeshProUGUI healthText;

    private void Awake()
    {
        healthBarStartSize = healthBar.sizeDelta.x;
    }

    public void UpdateHealthUI(float currentHealth, float maxHealth)
    {
        healthText.text = currentHealth.ToString();
        healthBar.sizeDelta = new Vector2((currentHealth / maxHealth) * healthBarStartSize, healthBar.sizeDelta.y);
    }
}
