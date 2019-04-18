using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadButtEnemy : MonoBehaviour
{
    public float chargeVelocity, chargeCoolDown, range,distX,distY;
    public bool canCharge, isCharging, isInRange,isAttacking,isDead;
    //public AudioClip punch;
    private float LocalX, timeToCharge;
    Transform pl;
    Player ps;
    Rigidbody2D rb;
    Animator anim;
    AudioSource chargingEnemyAud;
    Vector2 dir, lastPos;
    EnemyHealth healthScript;

    void Start()
    {
        chargingEnemyAud = gameObject.GetComponent<AudioSource>();
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ps= GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        LocalX = transform.localScale.x;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthScript = GetComponent<EnemyHealth>();
        isDead = false;
    }

    void FixedUpdate()
    {
        if (pl != null)
        {
            if (Vector2.Distance(transform.position, ps.centrePoint.transform.position) <= range)
            {
                isInRange = true;
            }
            else
            {
                isInRange = false;
            }
            if (isInRange)
            {
                look();
                if (Time.time > timeToCharge)
                {
                    timeToCharge = Time.time + 1 / chargeCoolDown;
                    recPos();
                }
                if (canCharge)
                {
                    Charge();
                }
                if (Vector2.Distance(transform.position, lastPos) <= 0.05)
                {
                    isCharging = false;
                    canCharge = false;
                }
            }
        }
        distX = transform.position.x - ps.centrePoint.transform.position.x;
        distY = transform.position.y - ps.centrePoint.transform.position.y;
        anim.SetFloat("DiffX", distX);
        anim.SetFloat("DiffY", distY);
        anim.SetBool("Attack", isAttacking);
        anim.SetBool("Charging", isCharging);
        anim.SetBool("Dead", isDead);

        if (healthScript.health <= 0)
        {
            isDead = true;
        }
    }

    void Charge()
    {
        isCharging = true;
        transform.position = Vector2.MoveTowards(transform.position, lastPos, chargeVelocity * Time.deltaTime);
    }

    void recPos()
    {
        lastPos = ps.centrePoint.transform.position;
        canCharge = true;
    }

    void look()
    {
        if (ps.centrePoint.transform.position.x > transform.position.x)
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
        ps.health += 5f;
        yield return new WaitForSeconds(1);
        isAttacking = false;
        StopCoroutine(Damage());
    }

}
