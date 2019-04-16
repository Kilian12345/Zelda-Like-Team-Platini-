using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public AudioSource enemy2Audio;
    public AudioClip punch;
    public GameObject gun, shootPoint;
    Player pm;
    public bool isInRange;
    public float range,combatDistance, enemyDamage,attackPushForce;
    public bool isAttacking;
    Transform target;
    public Transform pl;
    public float angle;
    private float LocalX;
    public Animator anim,anim2;
    public float attackSpeed,attackRange;
    private float timeToAttack, attackCoolDown;
    Vector2 dir;
    Rigidbody2D rb;

    void Start()
    {
        attackPushForce = 500f;
        attackCoolDown = 1 / attackSpeed;
        enemy2Audio = gameObject.GetComponent<AudioSource>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        LocalX = transform.localScale.x;
        anim = gun.GetComponentInChildren<Animator>();
        anim2 = GetComponent<Animator>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) < range)
            {
                if (pm.EnemiesFollowing < 3 && !isAttacking)
                {
                    anim2.SetBool("isSeen", true);
                }
                else
                {
                    anim2.SetBool("isSeen", false);
                }
                if (Vector2.Distance(transform.position, target.position) < combatDistance)
                {
                    isInRange = true;
                    if (Time.time > timeToAttack)
                    {
                        timeToAttack = Time.time + 1 / attackSpeed;
                        isAttacking = true;
                        StartCoroutine(Attack());
                        anim.SetBool("Hit", true);
                        Invoke("switchAttackBool", attackCoolDown/2);
                    }

                }
                else
                {
                    isInRange = false;
                    anim.SetBool("Hit", false);
                }
                look();
            }
            else
            {
                anim2.SetBool("isSeen", false);
            }
        }

    }

    void switchAttackBool()
    {
        isAttacking = false;
    }

    void look()
    {
        dir = (pl.position - transform.position);
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (pl.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(LocalX, transform.localScale.y, transform.localScale.z);
            gun.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            transform.localScale = new Vector3(-LocalX, transform.localScale.y, transform.localScale.z);
            gun.transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
        }
    }

    /*void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            GetComponent<EnemyHealth>().health = 0;
        }
    }*/

    IEnumerator Attack()
    {
        
        Collider2D[] enemiestoDamage = Physics2D.OverlapCircleAll(shootPoint.transform.position, attackRange);
        //Debug.Log(enemiestoDamage.Length);
        if (enemiestoDamage.Length > 0)
        {


            for (int i = 0; i < enemiestoDamage.Length; i++)
            {
                if (enemiestoDamage[i].GetComponent<Player>() != null)
                {
                    enemiestoDamage[i].GetComponent<Player>().health+=enemyDamage;
                    //enemiestoDamage[i].GetComponent<Rigidbody2D>().AddForce(dir.normalized * 500000, ForceMode2D.Impulse);
                    enemiestoDamage[i].GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(dir.normalized.x, dir.normalized.y) * attackPushForce, ForceMode2D.Impulse);
                    yield return new WaitForSeconds(0.1f);
                    enemiestoDamage[i].GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    break;
                }
            }
        }
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Hit", false);
        StopCoroutine(Attack());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(shootPoint.transform.position, attackRange);
    }

    void OnDestroy()
    {
        pm.EnemiesFollowing--;
    }
}

