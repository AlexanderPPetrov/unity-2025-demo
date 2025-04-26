using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public GameObject objectToSpawn;

    public float spawnDelay = 2f;
    public float spawnOffsetX = 5f;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
       while(true)
        {

            float randomX = UnityEngine.Random.Range(-spawnOffsetX, spawnOffsetX);
            Vector3 spawnPosition = transform.position + new Vector3(randomX, 0f, 0f);
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

            Debug.Log("Spawn");
            yield return new WaitForSeconds(spawnDelay);
        }
    }

}
