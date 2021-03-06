﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kami : MonoBehaviour
{
    public int difficultyLevel; ///////// difficulty
    public float detectionRange;
    public float stoppingDistance;
    public float moveSpeed;
    public float damageRange;
    public float damageValue;
    public float damagePushForce;
    public float waitForExplode;

    public bool isInRange;
    public bool canExplode;
    public bool startExplode;
    private bool dialogue;


    private float LocalX;
    private float angle;

    private Vector2 dir;

    private bool isMoving;
    private bool isExploding;

    Player plScript;
    Animator anim;
    EnemyHealth healthScript;
    [SerializeField] Transform center;
    DialogueManager dm;

    void Start()
    {
        LocalX = transform.localScale.x;
        anim = gameObject.GetComponent<Animator>();
        plScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        healthScript = GetComponent<EnemyHealth>();
        dm = GameObject.FindGameObjectWithTag("Dialogue_Manager").GetComponent<DialogueManager>();
    }

    void Update()
    {
        dialogue = dm.DialogueCheck;
        if (!dialogue)
        {
            animate();
        }
    }

    void FixedUpdate()
    {
        if (!dialogue)
        {
            look();
            if (Vector2.Distance(center.position, plScript.centrePoint.transform.position) <= detectionRange)
            {
                isInRange = true;
            }

            if (canExplode)
            {
                if (!startExplode)
                {
                    StartCoroutine(Explode());
                }
            }
            else
            {
                if (isInRange)
                {
                    move();
                }
            }
            if (healthScript.health <= 0)
            {
                canExplode = false;
                isExploding = true;
            }
        }
            
        
    }

    void move()
    {
        if (Vector2.Distance(center.position, plScript.centrePoint.transform.position) > stoppingDistance)
        { 
            transform.position = Vector2.MoveTowards(transform.position, plScript.centrePoint.transform.position, moveSpeed * Time.deltaTime);
            isMoving = true;
        }
        else
        {
            isMoving = false;
            canExplode = true;
        }
    }

    void look()
    {
        if (plScript.centrePoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(LocalX, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-LocalX, transform.localScale.y, transform.localScale.z);
        }
    }

    void animate()
    {
        anim.SetBool("Moving", isMoving);
        anim.SetBool("Exploding", isExploding);

    }


    IEnumerator Explode()
    {
        startExplode = true;
        yield return new WaitForSeconds(waitForExplode);
        
        isExploding = true;
        dir = (plScript.centrePoint.transform.position - transform.position).normalized;
        Debug.Log("Exploded Boi " + new Vector2(/*Mathf.Round*/(dir.x), /*Mathf.Round*/(dir.y)));
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Collider2D[] enemiestoDamage = Physics2D.OverlapCircleAll(center.position, damageRange);
        if (enemiestoDamage.Length > 0)
        {
            for (int i = 0; i < enemiestoDamage.Length; i++)
            {
                if (enemiestoDamage[i].GetComponent<Player>() != null)
                {
                    enemiestoDamage[i].GetComponent<PlayerController>().isPushed = true;
                    enemiestoDamage[i].GetComponent<Player>().TakeDamage(damageValue);
                    enemiestoDamage[i].GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(/*Mathf.Round*/(dir.x) *damagePushForce, /*Mathf.Round*/(dir.y) * damagePushForce) , ForceMode2D.Impulse);
                    Debug.Log("Impacted Boi " + new Vector2(/*Mathf.Round*/(dir.x) * damagePushForce, /*Mathf.Round*/(dir.y) * damagePushForce));
                    yield return new WaitForSeconds(0.0125f);
                    enemiestoDamage[i].GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    enemiestoDamage[i].GetComponent<PlayerController>().isPushed = false;
                    break;
                }
            }
        }
        healthScript.health = 0;
        yield return new WaitForSeconds(0.5f);
        StopCoroutine(Explode());
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center.position, damageRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(center.position, detectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(center.position, stoppingDistance);       
    }

}
