using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManger : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnCooldown = 10f;
    public float cooldownReducer = 0.1f;
    public float minSpawnTime = 0.1f;
    public float radius;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExecuteEveryTenSeconds());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnNewEnemy()
    {
       Vector3 spawnPosition = RandomSpawn() + Vector3.up * 2;
       GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        
    }

    private Vector3 RandomSpawn()
    {
        Vector3 randomSpawn = new Vector3(Random.Range(-1f,1f),0, Random.Range(-1f, 1f)).normalized * radius;
        return randomSpawn;
    }

    IEnumerator ExecuteEveryTenSeconds()
    {
        while (true)
        {
            SpawnNewEnemy();
            if (spawnCooldown - cooldownReducer > minSpawnTime)
            {
                spawnCooldown -= cooldownReducer;
            }
            yield return new WaitForSeconds(spawnCooldown);
        }
    }


}
