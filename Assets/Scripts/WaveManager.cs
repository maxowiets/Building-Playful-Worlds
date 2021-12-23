using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    int currentWave = 0;
    int currentEnemies = 0;
    public float baseEnemyAmount;
    public Spawner[] spawners;
    public float spawnDuration;
    public GameObject[] enemies;

    public float timerBetweenWaves;
    float timeBetweenWaves;
    bool waveIsActive;

    public TextMeshProUGUI currentWaveText;
    public TextMeshProUGUI currentEnemiesText;

    void Update()
    {
        if (!waveIsActive)
        {
            timeBetweenWaves += Time.deltaTime;
            if (timeBetweenWaves >= timerBetweenWaves)
            {
                timeBetweenWaves = 0;
                waveIsActive = true;
                StartCoroutine(SpawnNewWave(Mathf.CeilToInt(baseEnemyAmount * (1 + PlayerStats.totalBuffs * 0.1f))));
            }
        }
    }

    IEnumerator SpawnNewWave(int totalEnemies)
    {
        currentWave++;
        UpdateWaveUI();
        float timeBetweenSpawning = spawnDuration / totalEnemies;

        GameObject enemy = enemies[0];

        var bossWave = currentWave.CalculateRemainder(10);
        var fastWave = currentWave.CalculateRemainder(5);
        var fatWave = currentWave.CalculateRemainder(4);

        if (bossWave == 0 )
        {
            enemy = enemies[3];
            totalEnemies = Mathf.CeilToInt(totalEnemies * 0.1f);
        }
        else if (fastWave == 0)
        {
            enemy = enemies[1];
            totalEnemies = Mathf.CeilToInt(totalEnemies * 0.5f);
        }
        else if (fatWave == 0)
        {
            enemy = enemies[2];
            totalEnemies = Mathf.CeilToInt(totalEnemies * 0.3f);
        }

        currentEnemies = totalEnemies;
        UpdateEnemiesUI();

        for (int i = 0; i < totalEnemies; i++)
        {
            spawners[Random.Range(0, spawners.Length)].SpawnEnemy(enemy);
            yield return new WaitForSeconds(timeBetweenSpawning);
        }
    }

    public void DecreaseEnemiesToKill()
    {
        currentEnemies--;
        if (currentEnemies <= 0)
        {
            waveIsActive = false;
        }
        UpdateEnemiesUI();
    }

    void UpdateEnemiesUI()
    {
        currentEnemiesText.text = "Enemies: " + currentEnemies.ToString();
    }

    void UpdateWaveUI()
    {
        currentWaveText.text = "Wave: " + currentWave.ToString();
    }
}
