using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public AudioSource enemy2Audio;
    public AudioClip dead, punch;
    public GameObject gun,particles;
    Player pm;
    public bool isInRange;
    public float range,enemyDamage;
    public bool isRunning;
    Transform target;
    public Transform pl;
    public float angle;
    private float LocalX;
    public Animator anim;
    public float attackSpeed;
    private float timeToAttack;


    void Start()
    {
        enemy2Audio = gameObject.GetComponent<AudioSource>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        LocalX = transform.localScale.x;
        anim = gun.GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) < range)
            {
                isInRange = true;
            }
            else
            {
                isInRange = false;
            }
            look();
            if (isRunning)
            {
                gun.SetActive(true);
            }
            else
            {
                gun.SetActive(true);
            }
        }
        
    }

    void look()
    {
            Vector3 dir = (pl.position - transform.position);
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            
        if (pl.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(LocalX, transform.localScale.y, transform.localScale.z);
            gun.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            transform.localScale = new Vector3(-LocalX, transform.localScale.y, transform.localScale.z);
            gun.transform.rotation = Quaternion.AngleAxis(angle-120, Vector3.forward);
        }
    }

    /// <NotWorking>
    ///void OnCollisionEnter2D(Collision2D col)
    ///   {
    ///    if (col.gameObject.tag == "Player")
    ///    {
    ///        isRunning = true;
    ///     }
    ///    if (col.gameObject.tag == "Bullet")
    ///    {
    ///        Destroy(gameObject, 0f);
    ///     }
    ///   }
    ///    void OnCollisionStay2D(Collision2D col)
    ///{
    ///   if (col.gameObject.tag == "Player")
    ///   {
    ///isRunning = true;
    ///}
    ///}
    ///void OnCollisionExit2D(Collision2D col)
    ///{
    ///if (col.gameObject.tag == "Player")
    ///{
    ///    isRunning = false;
    ///  }
    ///}
    ///void Damage()
    ///{
    ///pm.health = -5;
    ///  isRunning = false;
    ///}
    /// 
    /// 
    /// </summary>


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            /*if (!isRunning)
            {
                StartCoroutine(Damage());
            }*/
            if (Time.time > timeToAttack)
            {
                timeToAttack = Time.time + 1 / attackSpeed;
                Attack();
            }

        }
        if (col.gameObject.tag == "Bullet")
        {
            enemy2Audio.clip = dead;
            enemy2Audio.Play();
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.5f);
        }
    }
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (Time.time > timeToAttack)
            {
                timeToAttack = Time.time + 1 / attackSpeed;
                Attack();  
            }
            /*if (!isRunning)
            {
                StartCoroutine(Damage());
            }*/

        }
        /*if (col.gameObject.tag !="Player")
        {
            StopCoroutine(Damage());
        }*/
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //StopCoroutine(Damage());
            isRunning = false;
            //anim.SetBool("Hit", false);
        }
    }
    IEnumerator Damage()
    {
        enemy2Audio.clip = punch;
        enemy2Audio.Play();
        isRunning = true;
        pm.health += enemyDamage;
        yield return new WaitForSeconds(3);
        isRunning = false;
    }

    void Attack()
    {
        anim.SetBool("Hit", true);
        enemy2Audio.clip = punch;
        enemy2Audio.Play();
        pm.health += enemyDamage;
    }
}

