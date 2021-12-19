using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCounterUI : MonoBehaviour
{
    TextMeshProUGUI ammoText;

    void Start()
    {
        ammoText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateAmmoText(string newAmmoText)
    {
        ammoText.text = newAmmoText;
    }
}
