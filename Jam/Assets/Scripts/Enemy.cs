using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    private float health;
    public float maxHealth = 100f;
    //position du chateau
    private Transform castlePos;
    public float moveSpeed = 2f;

    public bool isGood = true;
    public bool hasTraded = false;

    public int moneyScam;

    private GameObject attachedMail;
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

    public void Seek (Transform _target)
    {
        castlePos = _target;
    }

    private void MoveEnemiesToCastle()
    {
        Vector3 dir = castlePos.transform.position - transform.position;
        float distanceThisFrame = moveSpeed * Time.deltaTime;
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void MoveAway()
    {
        Vector3 dir = transform.position - castlePos.transform.position;
        float distanceThisFrame = moveSpeed * Time.deltaTime;
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        Destroy(gameObject,20f);
    }

    public void UpdateTradeStatus(bool _isGood,bool _hasTraded, GameObject _emailSwipe)
    {

        isGood = _isGood;
        hasTraded = _hasTraded;
        attachedMail = _emailSwipe;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            Turret turret = other.gameObject.GetComponent<Turret>();
            turret.playerReference.GetScam(moneyScam);
            attachedMail.GetComponent<EmailSwipe>().DestroyMail();
            Destroy(gameObject);           
        }
        
    }
}
