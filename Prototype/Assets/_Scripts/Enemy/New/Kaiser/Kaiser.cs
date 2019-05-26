using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaiser : MonoBehaviour
{

    #region Global Values

    [Header("Global Values")]
    public bool isActive;
    public float phase1ExitPercent;
    public float phase2ExitPercent;
    public int curPhase;
    public int curSubPhase;
    public int attackCtr;
    public bool isStarted;
    public bool isEnded;

    private float damageValue;
    [SerializeField]
    private float healthPercent;
    [SerializeField]
    private int curAttack;
    private float coinToss;
    private float LocalX;
    private bool activated;
    private bool start;
    private bool pause;
    private bool enterPhase2;
    private bool enterPhase3;
    private bool startPhase2;
    private bool startPhase3;
    private bool isCooling;
    #endregion

    #region Dash Variables

    [Header("Dash Values")]
    public float dashVelocity;
    public float dashCoolDown;
    public float dashDamageValue;

    public float dashCtr;
    public bool canDash;

    private float timeToDash;

    private bool isDashing;
    private bool isAttacking;
    private bool pauseDash;

    #endregion

    #region Jump Variables

    [Header("Jump Values")]
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

    public float jumpCtr;
    public bool canJump;

    private float timeToJump;
    private float timeparam;

    private bool isJumping;
    [SerializeField]
    private bool canDamage;
    private bool pauseJump;

    #endregion

    #region Precise Strike

    [Header("Precise Strike Values")]
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

    [Header("Bullet Hell")]
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

    // Scripted Camera
    public GameObject scriptedCamera;

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
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        coll = GetComponent<Collider2D>();

        isStarted = true;

    }

    void FixedUpdate()
    {
        startPoint = shootPoint.transform.position;
        if (isActive)
        {
            curPhase = 1;
            isActive = false;
            sR.enabled = true;
            Invoke("entry", 1f);
        }
        if (activated)
        {
            Invoke("idle", 2f);
        }
        if (start)
        {
            SwitchPhases();
        }
        animate();

        if (pauseDash || pauseFire || pauseFireBH || pauseJump)
        {
            pause = true;
        }
        else
        {
            pause = false;
        }
    }

    void SwitchPhases()
    {
        healthPercent = (healthScript.health / healthScript.maxHealth) * 100;
        if (healthPercent >= phase1ExitPercent)
        {
            StartCoroutine(Phase1());
            //attacks();
        }
        else if (healthPercent < phase1ExitPercent && healthPercent >= phase2ExitPercent)
        {
            if (!enterPhase2)
            {
                curPhase = 2;
                curSubPhase = 0;
                StopCoroutine(Phase1());
                if (!startPhase2)
                {
                    startPhase2 = true;
                    Invoke("transitionTo2", 3f);
                }
                //attacks();
            }
            else
            {
                //attacks();
                StartCoroutine(Phase2());
            }
        }
        else if (healthPercent < phase2ExitPercent && healthPercent > 0)
        {
            if (!enterPhase3)
            {
                curPhase = 3;
                curSubPhase = 0;
                StopCoroutine(Phase2());
                if (!startPhase3)
                {
                    startPhase3 = true;
                    Invoke("transitionTo3", 3f);
                }
                //attacks();
            }
            else
            {
                //attacks();
                StartCoroutine(Phase3());
            }
        }
        else
        {

            isEnded = true;
            StopCoroutine(Phase3());
            anim.SetBool("Death", true);


        }
    }

    void transitionTo2()
    {
        enterPhase2 = true;
        timeToJump = 0;
        timeToDash = 0;
        timeToFire = 0;
        timeToFireBH = 0;
        attackCtr = 0;
        curSubPhase = 1;
        Debug.Log("Phase2");
    }

    void transitionTo3()
    {
        projectileSpeed = 0.8f;
        dashCoolDown = 0.3f;
        numberOfProjectiles = 12;
        enterPhase3 = true;
        timeToJump = 0;
        timeToDash = 0;
        timeToFire = 0;
        timeToFireBH = 0;
        attackCtr = 0;
        curSubPhase = 1;
        Debug.Log("Phase3");
    }

    IEnumerator Phase1()
    {
        if (curSubPhase <= 2 && curPhase == 1)
        {
            switch (curSubPhase)
            {
                case 1:
                    {
                        curAttack = 1;
                        attacks();
                    }
                    break;
                case 2:
                    {
                        curAttack = 2;
                        attacks();
                    }
                    break;
            }
        }
        else
        {
            yield return new WaitForSeconds(3f);
            curSubPhase = 1;
        }
        StopCoroutine(Phase1());
    }
    IEnumerator Phase2()
    {
        if (curSubPhase <= 2 && curPhase == 2)
        {
            switch (curSubPhase)
            {
                case 1:
                    {
                        curAttack = 3;
                        attacks();
                    }
                    break;
                case 2:
                    {
                        curAttack = 4;
                        attacks();
                    }
                    break;
            }
        }
        else
        {
            yield return new WaitForSeconds(3f);
            curSubPhase = 1;
        }
        StopCoroutine(Phase2());
    }

    IEnumerator Phase3()
    {
        if (curSubPhase <= 4 && curPhase == 3)
        {
            switch (curSubPhase)
            {
                case 1:
                    {
                        curAttack = 3;
                        attacks();
                    }
                    break;
                case 2:
                    {
                        curAttack = 2;
                        attacks();
                    }
                    break;
                case 3:
                    {
                        curAttack = 3;
                        attacks();
                    }
                    break;
                case 4:
                    {
                        curAttack = 4;
                        attacks();
                    }
                    break;
            }
        }
        else
        {
            yield return new WaitForSeconds(3f);
            curSubPhase = 1;
        }
        StopCoroutine(Phase3());
    }

    void attacks()
    {
        switch (curAttack)
        {
            case 1:
                {
                    damageValue = jumpDamageValue;
                    StartJump();
                    if (attackCtr < jumpCtr)
                    {
                        if (Time.time > timeToJump)
                        {
                            attackCtr++;
                            timeToJump = Time.time + 1 / jumpRate;
                            jumpPos();
                        }
                    }
                    else
                    {
                        if (!pauseJump)
                        {
                            pauseJump = true;
                            isCooling = true;
                            Debug.Log("Reseting COOl jump");
                            Invoke("coolDown", 2f);
                        }
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
                            Debug.Log("Reseting COOl precise");
                            Invoke("coolDown", 2f);

                        }

                    }
                }
                break;
            case 3:
                {
                    damageValue = dashDamageValue;
                    StartDash();
                    if (attackCtr < dashCtr)
                    {
                        if (Time.time > timeToDash)
                        {
                            attackCtr++;
                            timeToDash = Time.time + 1 / dashCoolDown;
                            recPos();
                        }
                    }
                    else
                    {
                        if (!pauseDash)
                        {
                            pauseDash = true;
                            Debug.Log("Reseting COOl dash");
                            Invoke("coolDown", 2f);
                        }
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
                            ctrBulletBH++;
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

                        }
                    }
                    else
                    {
                        if (!pauseFireBH)
                        {
                            pauseFireBH = true;
                            firstShot = false;
                            Debug.Log("Reseting COOl bh");
                            Invoke("coolDown", 2f);
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
        if (Vector2.Distance(transform.position, lastPos) <= 0.1)
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
        pauseDash = false;
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
        pauseJump = false;
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
        cp[1].position = new Vector2((cp[0].position.x + cp[2].position.x) / 2, ((cp[0].position.y + cp[2].position.y) / 2 + jumpHeightModifier));
        //cp[1].position = new Vector2((cp[0].position.x + cp[2].position.x) / 2, ((cp[0].position.y + cp[2].position.y) / 2) + jumpHeightModifier * (Vector2.Distance(cp[0].position, cp[2].position) / jumpRange));

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
        attackCtr = 0;
        curSubPhase++;
        Debug.Log("Reseting COOl");
    }

    IEnumerator Shoot()
    {
        isCooling = false;
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
        isCooling = false;
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
            if (!isAttacking && (isDashing || canDamage))
            {
                StartCoroutine(Damage());
            }
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!isAttacking && (isDashing || canDamage))
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
