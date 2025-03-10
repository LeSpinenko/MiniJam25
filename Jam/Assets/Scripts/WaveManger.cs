using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManger : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnCooldown;
    public float cooldownReducer;
    public float minSpawnTime;
    public float radius;

    public EmailManager emailManager;


    // Start is called before the first frame update
    void Start()
    {
        spawnCooldown = 10f;
        minSpawnTime = 0.1f;
        cooldownReducer = 0.1f;

        StartCoroutine(ExecuteEveryTenSeconds());
        StartCoroutine(ChangeCoolDown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnNewMarchand()
    {
       Vector3 spawnPosition = RandomSpawn() + Vector3.up * 2;
       GameObject marchand = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
       Enemy myEnemy = marchand.GetComponent<Enemy>();
       emailManager.SpawnNewEmail(myEnemy); 
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
            SpawnNewMarchand();
            yield return new WaitForSeconds(spawnCooldown);
        }
    }

        IEnumerator ChangeCoolDown()
    {
        while (true)
        {
            if (spawnCooldown - cooldownReducer > minSpawnTime)
            {
                spawnCooldown -= cooldownReducer;
            }
            yield return new WaitForSeconds(10f);
        }
    }


}