using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public GameObject[] otherUIElements;

    private void Start()
    {
        EnableDeathScreen();
    }

    public void EnableDeathScreen()
    {
        foreach (var obj in otherUIElements)
        {
            obj.SetActive(false);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
