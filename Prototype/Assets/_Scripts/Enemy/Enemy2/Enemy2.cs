using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public AudioSource enemy2Audio;
    public AudioClip dead, punch;
    public GameObject gun, shootPoint, particles;
    Player pm;
    public bool isInRange;
    public float range, enemyDamage;
    public bool isAttacking;
    Transform target;
    public Transform pl;
    public float angle;
    private float LocalX;
    public Animator anim;
    public float attackSpeed,attackRange;
    private float timeToAttack;
    Vector2 dir;
    Rigidbody2D rb;

    void Start()
    {
        enemy2Audio = gameObject.GetComponent<AudioSource>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        LocalX = transform.localScale.x;
        anim = gun.GetComponentInChildren<Animator>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) < range)
            {
                isInRange = true;
                if (Time.time > timeToAttack)
                {
                    timeToAttack = Time.time + 1 / attackSpeed;
                    StartCoroutine(Attack());
                    anim.SetBool("Hit", true);
                }
                
            }
            else
            {
                isInRange = false;
                anim.SetBool("Hit", false);
            }
            look();
        }
        

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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            enemy2Audio.clip = dead;
            enemy2Audio.Play();
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.5f);
        }
    }

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
                    isAttacking = false;
                    break;
                }
            }
            isAttacking = false;
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

