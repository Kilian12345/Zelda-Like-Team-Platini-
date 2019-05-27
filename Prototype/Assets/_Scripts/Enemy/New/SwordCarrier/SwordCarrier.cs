using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCarrier : MonoBehaviour
{
    public int difficultyLevel; ///////// difficulty
    public AudioClip swordatk;
    public GameObject gun;
    public GameObject shootPoint;

    public float moveSpeed;
    public float combatRange;
    public float chasingRange;
    public float stoppingDistance;
    public float enemyDamage;
    public float attackSpeed;
    public float attackRange;
    public float attackPushForce;
    public float angle;

    public bool isInChaseRange;
    public bool isInAttackRange;
    public bool canAttack;
    public bool canMove;

    private float timeToAttack;
    private float attackCoolDown;
    private float LocalX;

    private bool isMoving;
    private bool isAttacking;
    private bool isDead;
    private bool isFollowing;
    private bool isActive;

    Player plScript;
    PlayerController plControl;
    Transform target;

    Animator anim;
    Rigidbody2D rb;
    EnemyHealth healthScript;
    [SerializeField] AudioSource enemyAudio;
    FeedBack_Manager fb_mana;

    Vector2 dir;

    [SerializeField] Transform center;



    void Start()
    {
        anim = GetComponent<Animator>();
        healthScript = GetComponent<EnemyHealth>();

        fb_mana = FindObjectOfType<FeedBack_Manager>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        plScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().centrePoint.transform;
        LocalX = transform.localScale.x;
        attackCoolDown = 1 / attackSpeed;

    }


    void Update()
    {
        animate();
    }

    void FixedUpdate()
    {
        
        if (target != null)
        {
            if (Vector2.Distance(center.position, plScript.centrePoint.transform.position) <= chasingRange)
            {
                if (!isActive && plScript.EnemiesFollowing < plScript.enemyFollowLimit)
                {
                    plScript.EnemiesFollowing++;
                    isActive = true;
                }
                if (isActive)
                {
                    Invoke("activate", 1f);
                }
                if (isFollowing)
                {
                    if (Vector2.Distance(center.position, plScript.centrePoint.transform.position) <= combatRange)
                    {
                        isInAttackRange = true;
                        isInChaseRange = true;
                    }
                    else
                    {
                        isInAttackRange = false;
                        isInChaseRange = true;
                    }
                }
            }
            else
            {
                isActive = false;
                isInChaseRange = false;
                isInAttackRange = false;
                isMoving = false;
                if (isFollowing)
                {
                    isFollowing = false;
                    plScript.EnemiesFollowing--;
                }
                else
                {
                    isMoving = false;
                    isAttacking = false;
                }
            }

            if (isInChaseRange)
            {

                if (!canAttack && isFollowing)
                {
                    move();
                }
                look();

                /*if (isInAttackRange)
                {
                    if (Time.time > timeToAttack)
                    {
                        timeToAttack = Time.time + 1 / attackSpeed;
                        canAttack = true;
                        StartCoroutine(Attack());
                        Invoke("switchMoveBool", attackCoolDown/3);
                    }
                }
                else
                {
                    isAttacking = false;
                    //canMove = true;
                }*/
            }

        }

    }

    void activate()
    {
        isFollowing = true;
    }

    bool AnimatorIsPlaying(string stateName)
    {
        return AnimatorIsPlaying() && anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    bool AnimatorIsPlaying()
    {
        return anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }


    void switchMoveBool()
    {
        //isAttacking = false;
        canAttack = false; ;
    }

    void delayedAttack()
    {
        if (!isAttacking)
        {
            StartCoroutine(Attack());
            Invoke("switchMoveBool", attackCoolDown / 3);
        }

    }

    void look()
    {
        dir = (target.position - transform.position);
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (target.position.x > transform.position.x)
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

    void animate()
    {
        if (healthScript.health <= 0)
        {
            if (plControl)
            {
                if (Vector2.Distance(center.position, plScript.centrePoint.transform.position) <= combatRange && plControl.isPushed)
                {
                    plControl.isPushed = false;
                }
            }
            isDead = true;
        }
        anim.SetBool("Active", isActive);
        anim.SetBool("Attacking", isAttacking);
        anim.SetBool("Moving", isMoving);
        anim.SetBool("Death", isDead);
    }

    void move()
    {
        if (Vector2.Distance(center.position, plScript.centrePoint.transform.position) > stoppingDistance)
        {
            isMoving = true;
            transform.position = Vector2.MoveTowards(transform.position, plScript.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            canAttack = true;
            Invoke("delayedAttack", 1f);
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        Collider2D[] enemiestoDamage = Physics2D.OverlapCircleAll(shootPoint.transform.position, attackRange);
        yield return new WaitForSeconds(0.5f);

        enemyAudio.clip = swordatk;
        enemyAudio.Play();

        //Debug.Log(enemiestoDamage.Length);
        if (enemiestoDamage.Length > 0)
        {
            for (int i = 0; i < enemiestoDamage.Length; i++)
            {
                if (enemiestoDamage[i].GetComponent<Player>() != null)
                {
                    fb_mana.StartCoroutine(fb_mana.vibrateBrève(0.15f, 0, 0.35f));
                    enemiestoDamage[i].GetComponent<PlayerController>().isPushed = true;
                    enemiestoDamage[i].GetComponent<Player>().health += enemyDamage;
                    enemiestoDamage[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(dir.normalized.x, dir.normalized.y) * attackPushForce, ForceMode2D.Impulse);
                    Debug.Log("Impacted Boi " + new Vector2(/*Mathf.Round*/(dir.normalized.x) * attackPushForce, /*Mathf.Round*/(dir.normalized.y) * attackPushForce));
                    yield return new WaitForSeconds(0.1f);
                    enemiestoDamage[i].GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    enemiestoDamage[i].GetComponent<PlayerController>().isPushed = false;
                    break;
                }
            }
        }
        isAttacking = false;
        yield return new WaitForSeconds(0.2f);
        StopCoroutine(Attack());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(shootPoint.transform.position, attackRange);
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(center.position, stoppingDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(center.position, chasingRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center.position, combatRange);
    }

    void OnDestroy()
    {
        if (plControl)
        {
            if (Vector2.Distance(center.position, plScript.centrePoint.transform.position) <= combatRange && plControl.isPushed)
            {
                plControl.isPushed = false;
            }
        }
        plScript.EnemiesFollowing--;
        
        
    }
}
