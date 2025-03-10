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

    public bool isGood = true;
    public bool hasTraded = false;

    private EmailData emailData;
    // Start is called before the first frame update
    void Start()
    {
        //isEnemy = true;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if((isGood && !hasTraded) || !isGood)
        {
            MoveEnemiesToCastle();
        }
        else
        {
            MoveAway();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void MoveEnemiesToCastle()
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

    private void MoveAway()
    {
        Vector3 dir = transform.position - castlePos;
        float distanceThisFrame = moveSpeed * Time.deltaTime;
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        Destroy(gameObject,20f);
    }

    private void Scam()
    {
        Destroy(gameObject);
        //Debug.Log("You got scammed sucker");
    }

    public void UpdateEmailData(EmailData _emailData)
    {
        emailData = _emailData;
        hasTraded = emailData.hasTraded;
        isGood = emailData.isGood;
    }
}
