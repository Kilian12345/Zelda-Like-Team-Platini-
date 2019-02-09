using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public Transform[] wayPoints;
    public GameObject bull,shootPoint;
    public float moveSpeed;
    public float angle;
    public bool pingPong;
    public bool isInRange;
    public Transform pl;
    public float FireRate = 0.5f;
    float timeToFire = 0;

    [HideInInspector]
    public bool reachedEnd;

    [HideInInspector]
    public Animator anim;

    [HideInInspector]
    public int curPoint;

    // Use this for initialization
    void Start()
    {
        curPoint = 0;
        transform.position = wayPoints[curPoint].transform.position;
        anim = gameObject.GetComponent<Animator>();
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
        look();
        anim.SetBool("isSeen", isInRange);
        if (isInRange)
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
        if (isInRange)
        {
            Vector3 dir = pl.position - transform.position;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            Vector3 dir = wayPoints[curPoint].position - transform.position;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
    }

    void move()
    {
        if (pingPong)
        {
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[curPoint].transform.position, moveSpeed * Time.deltaTime);
            if (reachedEnd)
            {
                if (transform.position == wayPoints[curPoint].transform.position)
                {
                    if (curPoint > 0)
                        curPoint -= 1;
                }
            }
            else
            {
                if (transform.position == wayPoints[curPoint].transform.position)
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
            if (transform.position == wayPoints[curPoint].transform.position)
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isInRange = true;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isInRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isInRange = false;
        }
    }
    void OnDestroy()
    {
        Destroy(transform.parent.gameObject); 
    }
}
