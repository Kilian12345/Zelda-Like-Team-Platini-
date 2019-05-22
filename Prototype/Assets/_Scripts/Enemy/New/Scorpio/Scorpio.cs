using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorpio : MonoBehaviour
{
    public int difficultyLevel; ///////// difficulty
    public Transform[] wayPoints;
    public GameObject bull, shootPoint;
    public float moveSpeed;
    public float bulSpeed;
    public float shootingRange;
    public float angle;
    public bool pingPong;
    public bool isInShootingRange;
    public float FireRate = 0.5f;
    float timeToFire = 0;

    [HideInInspector]
    public bool reachedEnd;
    private bool isShooting, isDead,isMoving;
    private float LocalX, distX, distY;

    Player plScript;
    Animator anim;
    EnemyHealth healthScript;
    [SerializeField] Transform center;

    //[HideInInspector]
    public int curPoint;

    void Start()
    {
        curPoint = 0;
        transform.position = wayPoints[curPoint].transform.position;
        LocalX = transform.localScale.x;
        anim = gameObject.GetComponent<Animator>();
        plScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        healthScript = GetComponent<EnemyHealth>();
    }


    void FixedUpdate()
    {
        look();
        animate();
        if (Vector2.Distance(center.position, plScript.centrePoint.transform.position) <= shootingRange)
        {
            if (Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / FireRate;
                StartCoroutine(Shoot());
            }
            isMoving = false;
        }
        else
        {
            patrol();
            isMoving = true;
        }
    }

    void animate()
    {
        if (healthScript.health <= 0)
        {
            isDead = true;
        }
        anim.SetFloat("Ver", distY);
        anim.SetBool("Shooting", isShooting);
        anim.SetBool("Moving", isMoving);
        anim.SetBool("Death", isDead);
    }


    void look()
    {
        if (!isMoving)
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
            distX = wayPoints[curPoint].transform.position.x - transform.position.x;
            distY = wayPoints[curPoint].transform.position.y - transform.position.y;
            if (distX>0)
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
        isShooting = false;
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

    /*void shoot()
    {
        //Debug.Log("Shoot Bitch!");
        isShooting = true;
        Vector2 dir = (plScript.centrePoint.transform.position - transform.position).normalized;
        GameObject bullet = Instantiate(bull, shootPoint.transform.position, Quaternion.identity);
        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), bullet.GetComponent<CircleCollider2D>());
        bullet.GetComponent<Rigidbody2D>().velocity = dir * bulSpeed;
    }*/

    IEnumerator Shoot()
    {
        isShooting = true;
        yield return new WaitForSeconds(0.1f);
        Vector2 dir = (plScript.centrePoint.transform.position - center.position).normalized;
        GameObject bullet = Instantiate(bull, shootPoint.transform.position, Quaternion.identity);
        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), bullet.GetComponent<CircleCollider2D>());
        bullet.GetComponent<Rigidbody2D>().velocity = dir * bulSpeed;
        yield return new WaitForSeconds(0.25f);
        isShooting = false;
        StopCoroutine(Shoot());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
