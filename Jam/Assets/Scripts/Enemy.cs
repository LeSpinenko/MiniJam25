using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    private float health;
    public float maxHealth = 100f;
    //position du chateau
    private Vector3 castlePos = new Vector3(0f,2f,0f);
    public float moveSpeed = 2f;

    public bool isEnemy;
    // Start is called before the first frame update
    void Start()
    {
        //isEnemy = true;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemies();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void MoveEnemies()
    {
        Vector3 dir = castlePos - transform.position;
        float distanceThisFrame = moveSpeed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            Scam();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void Scam()
    {
        Destroy(gameObject);
        //Debug.Log("You got scammed sucker");
    }
}
