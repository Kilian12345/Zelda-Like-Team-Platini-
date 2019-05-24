using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaiser : MonoBehaviour
{

    #region Global Values

    public bool isActive;
    public float phase1ExitPercent;
    public float phase2ExitPercent;
    public int curPhase;
    public int jumpCtr;
    public int preciseStrikeCtr;
    public int dashCtr;
    public int bulletHellCtr;
    public int attackCtr;

    private float damageValue;
    [SerializeField]
    private int attackType;
    private float healthPercent;
    [SerializeField]
    private int curAttack;
    private float coinToss;
    private float LocalX;
    private bool activated;
    private bool start;
    private bool pause;
    #endregion

    #region Dash Variables

    public float dashVelocity;
    public float dashCoolDown;
    public float dashDamageValue;

    public bool canDash;

    private float timeToDash;

    private bool isDashing;
    private bool isAttacking;

    #endregion

    #region Jump Variables

    public Transform[] cp;

    public float jumpSpeed;
    public float jumpHeightModifier;
    public float jumpRate;
    public float jumpRange;
    public float jumpDamageValue;

    private Transform playerPos;

    private Vector2 curPos;
    private Vector2 gizmoPos;
    private Vector2[] points;


    public bool canJump;

    private float timeToJump;
    private float timeparam;

    private bool isJumping;
    [SerializeField]
    private bool canDamage;

    #endregion

    #region Precise Strike
    public float bulSpeed;
    public float shotsPerBurst;
    public float shotRate;
    public float coolDownTime;
    public GameObject bull;
    public GameObject shootPoint;

    private float timeToFire = 0;
    [SerializeField]
    private float ctrBullet;
    private bool pauseFire;
    private bool isShooting;
    #endregion

    #region Bullet Hell

    public int numberOfProjectiles;
    public float projectileSpeed;
    public float shotsPerBurstBH;
    public float shotRateBH;
    public float coolDownTimeBH;
    public GameObject projectilePrefab;

    private Vector3 startPoint;
    private const float radius = 1f;
    [SerializeField]
    private float curAngle;
    private float angle;
    private float timeToFireBH = 0;
    [SerializeField]
    private float ctrBulletBH;
    private bool pauseFireBH;
    private bool firstShot;

    #endregion

    #region Misc Values

    Collider2D coll;
    Vector2 lastPos;
    Player plScript;
    Animator anim;
    EnemyHealth healthScript;
    SpriteRenderer sR;

    #endregion

    void Start()
    {
        healthScript = GetComponent<EnemyHealth>();
        sR = GetComponent<SpriteRenderer>();
        LocalX = transform.localScale.x;
        sR.enabled = false;
        isActive = true;

        plScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        anim = GetComponent<Animator>();

        cp[0].position = transform.position;
        playerPos = plScript.centrePoint.transform;
        coll = GetComponent<Collider2D>();
        
    }

    void FixedUpdate()
    {
        startPoint = shootPoint.transform.position;
        if (isActive)
        {
            isActive = false;
            sR.enabled = true;
            Invoke("entry", 1f);
        }
        if (activated)
        {
            Invoke("idle", 5f);
        }
        if (start)
        {
            healthPercent = (healthScript.health / healthScript.maxHealth) * 100;
        }
        animate();
        attacks();
        
        
        
        
    }

    void attacks()
    {
        switch (curAttack)
        {
            case 1:
                {
                    StartJump();
                    if (Time.time > timeToJump)
                    {
                        timeToJump = Time.time + 1 / jumpRate;
                        jumpPos();
                    }
                }
                break;
            case 2:
                {
                    StartPreciseStrike();
                    if (ctrBullet < shotsPerBurst)
                    {
                        if (Time.time > timeToFire)
                        {
                            ctrBullet++;
                            timeToFire = Time.time + 1 / shotRate;
                            StartCoroutine(Shoot());
                        }
                    }
                    else
                    {
                        if (!pauseFire)
                        {
                            pauseFire = true;
                            Invoke("coolDown", coolDownTime);
                        }

                    }
                }
                break;
            case 3:
                {
                    StartDash();
                    if (Time.time > timeToDash)
                    {
                        timeToDash = Time.time + 1 / dashCoolDown;
                        recPos();
                    }
                }
                break;
            case 4:
                {
                    StartBulletHell();
                    if (ctrBulletBH < shotsPerBurstBH)
                    {
                        if (Time.time > timeToFireBH)
                        {
                            timeToFireBH = Time.time + 1 / shotRateBH;
                            if (curAngle < 45)
                            {
                                curAngle += 4.3f;
                            }
                            else
                            {
                                curAngle = 0f;
                            }
                            spawnProjectiles(numberOfProjectiles, curAngle);
                            ctrBulletBH++;
                        }
                    }
                    else
                    {
                        if (!pauseFireBH)
                        {
                            pauseFireBH = true;
                            firstShot = false;
                            Invoke("coolDown", coolDownTimeBH);
                        }
                    }
                }
                break;
        }
    }

    void animate()
    {
        anim.SetBool("Active", activated);
        if (plScript.centrePoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(LocalX, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-LocalX, transform.localScale.y, transform.localScale.z);
        }
    }

    void entry()
    {
        activated = true;
    }

    void idle()
    {
        start = true;
    }



    #region Dash

    void StartDash()
    {
        if (canDash)
        {
            dash();
        }
        else
        {
            isDashing = false;
        }
        if (Vector2.Distance(transform.position, lastPos) <= 0.05)
        {
            isDashing = false;
            canDash = false;
        }
        if (isDashing)
        {
            anim.SetInteger("AttackType", 1);
        }
        else
        {
            anim.SetInteger("AttackType", 0);
        }
    }

    void dash()
    {
        isDashing = true;
        transform.position = Vector2.MoveTowards(transform.position, lastPos, dashVelocity * Time.deltaTime);
    }

    void recPos()
    {
        lastPos = plScript.centrePoint.transform.position;
        canDash = true;
    }

    #endregion

    #region Jump

    void StartJump()
    {
        if (canJump)
        {
            StartCoroutine(Jump());
        }

        if (isJumping)
        {
            anim.SetInteger("AttackType", 3);
        }
        else
        {
            anim.SetInteger("AttackType", 0);
        }
    }

    void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmoPos = (Mathf.Pow(1 - t, 2) * cp[0].position) + (2 * cp[1].position * (1 - t) * t) + (cp[2].position * Mathf.Pow(t, 2));
            //gizmoPos.x = (Mathf.Pow(1 - t, 2) * cp[0].position.x) + (2 * cp[1].position.x * (1 - t)) + (cp[2].position.x * t * t);
            //gizmoPos.y = (Mathf.Pow(1 - t, 2) * cp[0].position.y) + (2 * cp[1].position.y * (1 - t)) + (cp[2].position.y * t * t);
            Gizmos.DrawSphere(gizmoPos, 0.025f);
        }

        Gizmos.DrawLine(new Vector2(cp[0].position.x, cp[0].position.y), new Vector2(cp[1].position.x, cp[1].position.y));
        Gizmos.DrawLine(new Vector2(cp[1].position.x, cp[1].position.y), new Vector2(cp[2].position.x, cp[2].position.y));

    }


    IEnumerator Jump()
    {
        timeparam = 0;
        isJumping = true;
        canJump = false;
        coll.enabled = false;

        /*for (int i = 0; i < cp.Length; i++)
        {
            points[i] = cp[i].position;
        }*/

        while (timeparam < 1)
        {
            timeparam += Time.deltaTime * jumpSpeed;

            curPos = (Mathf.Pow(1 - timeparam, 2) * cp[0].position) + (2 * cp[1].position * (1 - timeparam) * timeparam) + (cp[2].position * Mathf.Pow(timeparam, 2));
            transform.position = curPos;

            yield return new WaitForEndOfFrame();
        }
        isJumping = false;
        canDamage = true;
        coll.enabled = true;
        Invoke("switchDamageBool", 0.5f);
        //timeparam = 0;
        //canJump = true;
    }

    void switchDamageBool()
    {
        canDamage = false;
    }

    void jumpPos()
    {
        canDamage = false;
        cp[0].position = transform.position;
        cp[2].position = playerPos.position;
        cp[1].position = new Vector2((cp[0].position.x + cp[2].position.x) / 2, ((cp[0].position.y + cp[2].position.y) / 2) + jumpHeightModifier * (Vector2.Distance(cp[0].position, cp[2].position) / jumpRange));

        canJump = true;
    }

    #endregion


    #region Precise Strike

    void StartPreciseStrike()
    {
        if (isShooting)
        {
            anim.SetInteger("AttackType", 2);
        }
        else
        {
            anim.SetInteger("AttackType", 0);
        }
    }

    void coolDown()
    {
        ctrBullet = 0;
        ctrBulletBH = 0;
    }

    IEnumerator Shoot()
    {
        isShooting = true;
        pauseFire = false;
        yield return new WaitForSeconds(1f);
        Vector2 dir = (plScript.centrePoint.transform.position - shootPoint.transform.position).normalized;
        GameObject bullet = Instantiate(bull, shootPoint.transform.position, Quaternion.identity);
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), bullet.GetComponent<CircleCollider2D>());
        bullet.GetComponent<Rigidbody2D>().velocity = dir * bulSpeed;
        isShooting = false;
        StopCoroutine(Shoot());
    }

    #endregion


    #region Bullet Hell

    void StartBulletHell()
    {
        if (!pauseFireBH)
        {
            anim.SetInteger("AttackType", 4);
        }
        else
        {
            anim.SetInteger("AttackType", 0);
        }
    }

    /*void coolDown()
    {
        ctrBullet = 0;
    }*/

    void spawnProjectiles(int numOfProj, float ang)
    {
        pauseFireBH = false;
        float angleStep = 360f / numOfProj;
        angle = ang;
        for (int i = 0; i < numOfProj; i++)
        {
            Debug.Log("Shot");
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
            Vector3 projectileMoveDirection = (projectileVector - startPoint).normalized * projectileSpeed;

            GameObject tmpObj = Instantiate(projectilePrefab, startPoint, Quaternion.identity);
            tmpObj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), tmpObj.GetComponent<CircleCollider2D>());

            angle += angleStep;

        }
    }

    #endregion
    #region Damage

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!isAttacking && isDashing)
            {
                StartCoroutine(Damage());
            }
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!isAttacking && isDashing)
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

    #endregion
}
