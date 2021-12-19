using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathCounterUI : MonoBehaviour
{
    int deathCounter;
    TextMeshProUGUI deathCounterText;

    private void Awake()
    {
        deathCounterText = GetComponent<TextMeshProUGUI>();
    }

    public void IncreaseDeathCounter()
    {
        deathCounter++;
        deathCounterText.text = deathCounter.ToString();
    }
}
