﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCock : MonoBehaviour
{
    public int difficultyLevel; ///////// difficulty
    public Transform[] wayPoints;
    public GameObject bull, shootPoint;
    public float moveSpeed;
    public float bulSpeed;
    public float shootingRange;
    public float chasingRange;
    public float bulletsPerBurst;
    public float coolDownTime;
    public float angle;
    public bool pingPong;
    public bool isInChaseRange;
    public bool isInShootingRange;
    public float FireRate = 0.5f;
    float timeToFire = 0;
    [SerializeField]
    private float ctrBullet;

    [HideInInspector]
    public bool reachedEnd, canPatrol;
    private bool isShooting, isMoving, isDead;
    private bool canMove;
    private bool isFollowing;
    private bool pauseFire;
    private bool dialogue;
    private float LocalX,distX,distY;

    Player plScript;
    Animator anim;
    EnemyHealth healthScript;
    [SerializeField] Transform center;
    DialogueManager dm;


    //[HideInInspector]
    public int curPoint;

    void Start()
    {
        curPoint = 0;
        canPatrol = true;
        canMove = true;
        transform.position = wayPoints[curPoint].transform.position;
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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dialogue)
        {
            look();
            if (Vector2.Distance(center.position, plScript.centrePoint.transform.position) <= chasingRange && plScript.EnemiesFollowing <= plScript.enemyFollowLimit)
            {
                canPatrol = false;
                if (Vector2.Distance(center.position, plScript.centrePoint.transform.position) <= shootingRange)
                {
                    isInChaseRange = true;
                    isInShootingRange = true;
                }
                else
                {
                    isInChaseRange = true;
                    isInShootingRange = false;
                    if (canMove)
                    {
                        move();
                    }
                }
            }
            else
            {
                if (isFollowing)
                {
                    plScript.EnemiesFollowing--;
                }
                isInShootingRange = false;
                isInChaseRange = false;
                if (canPatrol)
                {
                    patrol();
                }
                else if (canMove && Vector2.Distance(center.position, plScript.centrePoint.transform.position) <= (chasingRange * 1.5f))
                {
                    move();
                }
                else
                {
                    if (isFollowing)
                    {
                        isFollowing = false;
                        plScript.EnemiesFollowing--;
                    }
                }
            }

            if (isInShootingRange && isInChaseRange)
            {
                if (ctrBullet < bulletsPerBurst)
                {
                    if (Time.time > timeToFire)
                    {
                        canMove = false;
                        timeToFire = Time.time + 1 / FireRate;
                        //shoot();
                        StartCoroutine(Shoot());
                        ctrBullet++;
                        Invoke("switchShootBool", (1 / FireRate) / 2);
                        //Invoke("switchMoveBool", (1 / FireRate) / 2);
                    }
                }
                else
                {
                    canMove = false;
                    Invoke("switchMoveBool", coolDownTime);
                    if (!pauseFire)
                    {
                        pauseFire = true;
                        Invoke("coolDown", coolDownTime);
                    }
                }
            }
            if (!isInShootingRange && isInChaseRange)
            {
                canMove = true;
            }
        }
            
    }

    void animate()
    {
        if (canMove || canPatrol)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        if (healthScript.health <= 0)
        {
            isDead = true;
        }
        anim.SetFloat("Ver", distY);
        anim.SetBool("Shooting", isShooting);
        anim.SetBool("Moving", isMoving);
        anim.SetBool("Death", isDead);
    }

    void switchMoveBool()
    {
        canMove = true;
    }

    void switchShootBool()
    {
        isShooting = false;
    }

    void coolDown()
    {
        ctrBullet = 0;
    }

    void look()
    {
        if (!canPatrol)
        {
            distY = plScript.centrePoint.transform.position.y - transform.position.y;
            if (plScript.centrePoint.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(LocalX, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(-LocalX, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            distY = wayPoints[curPoint].transform.position.y - transform.position.y;
            if (wayPoints[curPoint].transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(LocalX, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(-LocalX, transform.localScale.y, transform.localScale.z);
            }
        }

    }

    void patrol()
    {
        if (pingPong)
        {
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[curPoint].transform.position, moveSpeed * Time.deltaTime);
            if (reachedEnd)
            {
                if (Vector2.Distance(center.position, wayPoints[curPoint].transform.position) <= 0)
                {
                    if (curPoint > 0)
                        curPoint -= 1;
                }
            }
            else
            {
                if (Vector2.Distance(center.position, wayPoints[curPoint].transform.position) <= 0)
                {
                    if (curPoint < wayPoints.Length - 1)
                        curPoint += 1;
                }
            }
            if (curPoint == wayPoints.Length - 1)
            {
                reachedEnd = true;
            }
            if (curPoint == 0)
            {
                reachedEnd = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[curPoint].transform.position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(center.position, wayPoints[curPoint].transform.position) <= 0)
            {
                curPoint += 1;
            }
            if (curPoint == wayPoints.Length)
            {
                curPoint = 0;
            }
        }
    }

    void move()
    {
        if (Vector2.Distance(center.position, plScript.centrePoint.transform.position) > shootingRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, plScript.centrePoint.transform.position, moveSpeed * Time.deltaTime);
            if (!isFollowing)
            {
                plScript.EnemiesFollowing++;
                isFollowing = true;
            }
        }
    }

    void shoot()
    {
        //Debug.Log("Shoot Bitch!");
        isShooting = true;
        Vector2 dir = (plScript.centrePoint.transform.position - transform.position).normalized;
        GameObject bullet = Instantiate(bull, shootPoint.transform.position, Quaternion.LookRotation(dir));
        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), bullet.GetComponent<CircleCollider2D>());
        bullet.GetComponent<Rigidbody2D>().velocity = dir * bulSpeed;
    }

    IEnumerator Shoot()
    {
        pauseFire = false;
        isShooting = true;

        Vector2 dir = (plScript.centrePoint.transform.position - shootPoint.transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject bullet = Instantiate(bull, shootPoint.transform.position, Quaternion.identity);
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), bullet.GetComponent<CircleCollider2D>());
        bullet.GetComponent<Rigidbody2D>().velocity = dir * bulSpeed;

        yield return new WaitForSeconds(0.25f);
        dir = (plScript.centrePoint.transform.position - shootPoint.transform.position).normalized;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bullet = Instantiate(bull, shootPoint.transform.position, Quaternion.identity);
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), bullet.GetComponent<CircleCollider2D>());
        bullet.GetComponent<Rigidbody2D>().velocity = dir * bulSpeed;

        yield return new WaitForSeconds(0.25f);
        dir = (plScript.centrePoint.transform.position - shootPoint.transform.position).normalized;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bullet = Instantiate(bull, shootPoint.transform.position, Quaternion.identity);
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), bullet.GetComponent<CircleCollider2D>());
        bullet.GetComponent<Rigidbody2D>().velocity = dir * bulSpeed;
        //canMove = true;
        //isShooting = false;
        StopCoroutine(Shoot());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center.position, shootingRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(center.position, chasingRange);
    }

    void OnDestroy()
    {
        if (isFollowing)
        {
            plScript.EnemiesFollowing--;
        }
    }
}
