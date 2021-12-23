using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
                if (_instance == null)
                {
                    Debug.LogError("Missing UI Manager");
                    return null;
                }
            }
            return _instance;
        }
    }

    private static UIManager _instance;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }
        _instance = this;
    }

    public AmmoCounterUI ammoCounterUI;
    public PlayerHealthUI playerHealthUI;
    public WaveManager waveManagerUI;
    public DeathScreen deathScreen;
    public BuffsUIManager buffUI;
}
