using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Vector2 spawnSize;

    public void SpawnEnemy(GameObject spawnableObject)
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnSize.x * 0.5f, spawnSize.x * 0.5f), 0, Random.Range(-spawnSize.y * 0.5f, spawnSize.y * 0.5f));
        Instantiate(spawnableObject, transform.position + spawnPosition, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(new Vector3(transform.position.x - spawnSize.x * 0.5f, transform.position.y, transform.position.z - spawnSize.y * 0.5f)
            , new Vector3(transform.position.x + spawnSize.x * 0.5f, transform.position.y, transform.position.z - spawnSize.y * 0.5f));

        Gizmos.DrawLine(new Vector3(transform.position.x - spawnSize.x * 0.5f, transform.position.y, transform.position.z - spawnSize.y * 0.5f)
            , new Vector3(transform.position.x - spawnSize.x * 0.5f, transform.position.y, transform.position.z + spawnSize.y * 0.5f));

        Gizmos.DrawLine(new Vector3(transform.position.x + spawnSize.x * 0.5f, transform.position.y, transform.position.z - spawnSize.y * 0.5f)
            , new Vector3(transform.position.x + spawnSize.x * 0.5f, transform.position.y, transform.position.z + spawnSize.y * 0.5f));

        Gizmos.DrawLine(new Vector3(transform.position.x - spawnSize.x * 0.5f, transform.position.y, transform.position.z + spawnSize.y * 0.5f)
            , new Vector3(transform.position.x + spawnSize.x * 0.5f, transform.position.y, transform.position.z + spawnSize.y * 0.5f));
    }
}
