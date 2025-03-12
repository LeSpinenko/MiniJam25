using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    private float health;
    public float maxHealth = 100f;
    //position du chateau
    private Transform castlePos;
    private float moveSpeed = 2.5f;
    private float runSpeed = 5f;

    public bool isGood = true;
    public bool hasTraded = false;
    private bool isAttacking = false;

    public int moneyScam;

    private GameObject attachedMail;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isGood", true);
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAttacking)
        {
            if (!isGood)
            {
                moveSpeed = runSpeed;
            }
            if(!hasTraded)
            {
                MoveEnemiesToCastle();
            }
            else
            {
                MoveAway();
            }
        }
        else 
        {
            return;
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
        Vector3 dir = castlePos.transform.position - transform.position;
        if(dir.x < 0){
            transform.GetChild(0).Rotate(0,180,0, Space.Self);
        }
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
        if(animator != null){
            animator.SetBool("isGood", _isGood);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            Turret turret = other.gameObject.GetComponent<Turret>();
            turret.playerReference.GetScam(moneyScam);
            if(attachedMail != null)
            {
                attachedMail.GetComponent<EmailSwipe>().DestroyMail();
            }
            isAttacking = true;            
            StartCoroutine(WaitAtCastle());                
        }
        
    }

    IEnumerator WaitAtCastle()
    {
        animator.SetBool("isAttacking", true);    
        yield return new WaitForSeconds(1f);
        animator.SetBool("isAttacking", false);
        isAttacking = false;
        hasTraded = true;
        isGood = false;
        animator.SetBool("isGood", false);
        //transform.Rotate(0,180,0);
        transform.GetChild(0).Rotate(0,180,0, Space.Self);
    }
}
