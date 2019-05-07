using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadButtEnemy : MonoBehaviour
{
    public float moveSpeed;
    public float chargeVelocity;
    public float chargeCoolDown;
    public float detectionRange;
    public float chargeRange;
    public float damageValue;

    public bool isInChaseRange;
    public bool isInChargeRange;
    public bool canCharge;
    
    //public AudioClip punch;


    private float LocalX;
    private float timeToCharge;
    private float distX;
    private float distY;

    private bool isCharging;
    private bool isAttacking;
    private bool isDead;
    private bool isFollowing;


    Transform pl;
    Player plScript;
    Rigidbody2D rb;
    Animator anim;
    AudioSource chargingEnemyAud;
    Vector2 dir, lastPos;
    EnemyHealth healthScript;

    void Start()
    {
        chargingEnemyAud = gameObject.GetComponent<AudioSource>();
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        plScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        LocalX = transform.localScale.x;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthScript = GetComponent<EnemyHealth>();
        isDead = false;
    }

    void FixedUpdate()
    {
        animate();
        if (pl != null)
        {
            if (Vector2.Distance(transform.position, plScript.centrePoint.transform.position) <= detectionRange)
            {
                if (!isFollowing && plScript.EnemiesFollowing < plScript.enemyFollowLimit)
                {
                    plScript.EnemiesFollowing++;
                    isFollowing = true;
                }
                if (isFollowing)
                {
                    if (Vector2.Distance(transform.position, plScript.centrePoint.transform.position) <= chargeRange)
                    {
                        isInChargeRange = true;
                        isInChaseRange = true;
                    }
                    else
                    {
                        isInChargeRange = false;
                        isInChaseRange = true;
                    }
                } 
            }
            else
            {
                isInChaseRange = false;
                isInChargeRange = false;
                if (isFollowing)
                {
                    isFollowing = false;
                    plScript.EnemiesFollowing--;
                }
            }

            if (isInChaseRange)
            {
                look();
                if (isInChargeRange)
                {
                    if (Time.time > timeToCharge)
                    {
                        timeToCharge = Time.time + 1 / chargeCoolDown;
                        recPos();
                    }
                    if (canCharge)
                    {
                        charge();
                    }
                    else
                    {
                        isCharging = false;
                    }
                    if (Vector2.Distance(transform.position, lastPos) <= 0.05)
                    {
                        isCharging = false;
                        canCharge = false;
                    }
                }
                else
                {
                    move();
                }
            }
            else
            {
                isCharging = false;
            }
        }

        if (healthScript.health <= 0 && healthScript.getDissolve == false)
        {
            isDead = true;
        }
    }

    void animate()
    {
        distX = transform.position.x - plScript.centrePoint.transform.position.x;
        distY = transform.position.y - plScript.centrePoint.transform.position.y;
        anim.SetFloat("DiffX", distX);
        anim.SetFloat("DiffY", distY);
        anim.SetBool("Attack", isAttacking);
        anim.SetBool("Charging", isCharging);
        anim.SetBool("Dead", isDead);
    }

    void move()
    {
        if (Vector2.Distance(transform.position, plScript.centrePoint.transform.position) > chargeRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, plScript.centrePoint.transform.position, moveSpeed * Time.deltaTime);
            isCharging = true;
        }
    }

    void charge()
    {
        isCharging = true;
        transform.position = Vector2.MoveTowards(transform.position, lastPos, chargeVelocity * Time.deltaTime);
    }

    void recPos()
    {
        lastPos = plScript.centrePoint.transform.position;
        canCharge = true;
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!isAttacking && isCharging)
            {
                StartCoroutine(Damage());
            }
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!isAttacking && isCharging)
            {
                StartCoroutine(Damage());
            }
        }
        if (col.gameObject.tag == "!Player")
        {
            StopCoroutine(Damage());
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            StopCoroutine(Damage());
            isAttacking = false;
        }
    }

    IEnumerator Damage()
    {
        isAttacking = true;
        plScript.TakeDamage(damageValue);
        yield return new WaitForSeconds(1);
        isAttacking = false;
        StopCoroutine(Damage());
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chargeRange);
    }

    void OnDestroy()
    {
        plScript.EnemiesFollowing--;
    }
}
