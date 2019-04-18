using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCock : MonoBehaviour
{

    public Transform[] wayPoints;
    public GameObject bull, shootPoint;
    public float moveSpeed;
    public float shootingRange;
    public float chasingRange;
    public float angle;
    public bool pingPong;
    public bool isInChaseRange;
    public bool isInShootingRange;
    public float FireRate = 0.5f;
    float timeToFire = 0;

    //[HideInInspector]
    public bool reachedEnd;
    private float LocalX;

    Player plScript;
    Animator anim;
    Transform plTransform;

    //[HideInInspector]
    public int curPoint;

    void Start()
    {
        curPoint = 0;
        transform.position = wayPoints[curPoint].transform.position;
        LocalX = transform.localScale.x;
        anim = gameObject.GetComponent<Animator>();
        plScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
        look();
        if (isInShootingRange && isInChaseRange)
        {
            if (Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / FireRate;
                shoot();
            }
        }
    }

    void look()
    {
        if (isInChaseRange)
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

    }

    void move()
    {
        if (pingPong)
        {
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[curPoint].transform.position, moveSpeed * Time.deltaTime);
            if (reachedEnd)
            {
                if (Vector2.Distance(transform.position, wayPoints[curPoint].transform.position) <= 0)
                {
                    if (curPoint > 0)
                        curPoint -= 1;
                }
            }
            else
            {
                if (Vector2.Distance(transform.position, wayPoints[curPoint].transform.position) <= 0)
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
            if (Vector2.Distance(transform.position, wayPoints[curPoint].transform.position) <= 0)
            {
                curPoint += 1;
            }
            if (curPoint == wayPoints.Length)
            {
                curPoint = 0;
            }
        }
    }
    void shoot()
    {
        Instantiate(bull, shootPoint.transform.position, Quaternion.identity);
    }
}
