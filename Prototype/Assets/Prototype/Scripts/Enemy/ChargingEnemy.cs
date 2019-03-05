using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingEnemy : MonoBehaviour
{
    public float chargeDuration, chargeVelocity, chargeCoolDown,range;
    public bool canCharge,isCharging,isInRange;
    public GameObject weapon, shootPoint, particles;
    public AudioClip dead, punch;
    private float LocalX,angle,timeToCharge;
    Transform pl;
    Rigidbody2D rb;
    AudioSource chargingEnemyAud;
    Vector2 dir,lastPos;

    void Start()
    {
        chargingEnemyAud = gameObject.GetComponent<AudioSource>();
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        LocalX = transform.localScale.x;
        rb= GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (pl != null)
        {
            if (Vector2.Distance(transform.position, pl.position) <= range)
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
                if (Vector2.Distance(transform.position, lastPos) <= 1)
                {
                    canCharge = false;
                }
            }

        }


    }

    void Charge()
    {
        transform.position = Vector2.MoveTowards(transform.position, lastPos, chargeVelocity * Time.deltaTime);
    }

    void recPos()
    {
        lastPos = pl.position;
        canCharge = true;
    }


    void look()
    {
        dir = (pl.position - transform.position);
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (pl.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(LocalX, transform.localScale.y, transform.localScale.z);
            weapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            transform.localScale = new Vector3(-LocalX, transform.localScale.y, transform.localScale.z);
            weapon.transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
        }
    }
    /*void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            chargingEnemyAud.clip = dead;
            chargingEnemyAud.Play();
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.5f);
        }
    }*/

}
