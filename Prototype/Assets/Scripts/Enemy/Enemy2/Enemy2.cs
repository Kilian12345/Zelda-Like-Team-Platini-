using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public AudioSource enemy2Audio;
    public AudioClip dead, punch;
    public GameObject gun,particles;
    PlayerMovement pm;
    public bool isInRange;
    public float range;
    public bool isRunning;
    public Animator anim;
    Transform target;
    public Transform pl;
    public float angle;

    void Start()
    {
        enemy2Audio = gameObject.GetComponent<AudioSource>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        anim = gameObject.GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            anim.SetBool("isSeen", isInRange);
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
                gun.SetActive(false);
            }
        }
        
    }

    void look()
    {
        if (isInRange)
        {
            Vector3 dir = (pl.position - transform.position);
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
            if (!isRunning)
            {
                StartCoroutine(Damage());
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
            if (!isRunning)
            {
                StartCoroutine(Damage());
            }
        }
        if (col.gameObject.tag !="Player")
        {
            StopCoroutine(Damage());
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            StopCoroutine(Damage());
            isRunning = false;
        }
    }
    IEnumerator Damage()
    {
        enemy2Audio.clip = punch;
        enemy2Audio.Play();
        isRunning = true;
        pm.health += 5f;
        yield return new WaitForSeconds(1);
        isRunning = false;
    }
}

