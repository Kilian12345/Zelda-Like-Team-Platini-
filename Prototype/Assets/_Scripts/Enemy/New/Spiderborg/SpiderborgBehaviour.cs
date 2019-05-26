using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderborgBehaviour : MonoBehaviour
{


    #region Variables
    [Header("General Variables")]
    public bool isStarted;
    public bool isEnded;
    public GameObject StrikeZone;
    [SerializeField] Animator anim;
    [SerializeField] EnemyHealth eh;

    public Rigidbody2D PlayerRb;
    public Rigidbody2D SpiderborgRb;
    public GameObject shield;

    public int SpiderState=150;
    public float speed = 2f;

    [SerializeField] int numStrike1;
    [SerializeField] int numStrike2;
    [SerializeField] int numStrike3;
    int i, j, k, animstate;
    [SerializeField] [Range(-190, 190)] float rangeAngle1;
    [SerializeField] [Range(-190, 190)] float rangeAngle2;
    [SerializeField] [Range(-190, 190)] float rangeAngle3;
    float healthPercent;

    [SerializeField] bool Small;
    [SerializeField] bool Medium;
    [SerializeField] bool Big;
    bool occupied = false;
    bool landed = false;

    [SerializeField] float cercleSize;
    [SerializeField] float timebtwatk = 1f;
    float cercleSize2, cercleSize3, attackdist, LocalX;
    Vector2 targetDirection;
    Player plScript;
    Transform target;
    Fow_Parent lazerAtk;
    #endregion

    #region Jump Variables
    [Header("Jump Variables")]
    public Transform[] cp;

    public float jumpSpeed;
    public float jumpHeightModifier;
    public float jumpRate;
    public float jumpRange;
    public float damageValue;

    private Transform playerPos;

    private Vector2 curPos;
    private Vector2 gizmoPos;
    private Vector2[] points;


    public bool canJump;

    private float timeToJump;
    private float timeparam;

    private bool isAttacking;
    private bool isJumping;
    [SerializeField]
    private bool canDamage;

    Collider2D coll;

    #endregion

    void Start()
    {
        plScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        lazerAtk = FindObjectOfType<Fow_Parent>();
        target = plScript.centrePoint.transform;
        LocalX = transform.localScale.x;
        shield.SetActive(false);
        anim.SetFloat("AnimX", 0f);
        i = numStrike1 - 1;
        j = numStrike2 - 1;
        k = numStrike3 - 1;
        PresetAtk3();
        attackdist = 1f;
        StartCoroutine("Landing");

        cp[0].position = transform.position;
        playerPos = plScript.centrePoint.transform;
        coll = GetComponent<Collider2D>();

        isStarted = true;
    }

    void FixedUpdate()
    {
        if(landed == true)
        {
            AnimationController();
        }

        if (occupied == false && landed == true)
        {
            CheckLife();
            StateSwitch();
        }
        look();
    }

    void CheckLife()
    {
        healthPercent = (eh.health / eh.maxHealth) * 100;
        if ((Vector2.Distance(transform.position, target.position) > attackdist) && occupied == false)
        {
            //Debug.Log("0");
            SpiderState = 0;
        }
        else if (healthPercent >= 70 && occupied == false)
        {
            //Debug.Log("1");
            SpiderState = 1;
            attackdist = 1.5f;
        }
        else if (healthPercent < 70 && healthPercent >= 35 && occupied == false)
        {
            //Debug.Log("2");
            SpiderState = 2;

            attackdist = 1f;
        }
        else if (healthPercent < 35 && healthPercent > 0 && occupied == false)
        {
            //Debug.Log("3");
            SpiderState = 3;
            attackdist = 0.7f;
        }
        else if (eh.health <= 0)
        {
            //Debug.Log("4");
            SpiderState = 4;

            isEnded = true;
        }

    }

    void StateSwitch()
    {
        switch (SpiderState)
        {
            case 0:
                Move();
                break;
            case 1:
                {
                    jumpUpdate();
                    StartCoroutine("ATK1");
                }
                break;
            case 2:
                StartCoroutine("ATK2");
                break;
            case 3:
                StartCoroutine("ATK3");
                break;
            case 4:
                {
                    StopAllCoroutines();
                }
                break;
        }
    }

    void Move()
    {
        Vector2 targetDirection = (target.position - transform.position).normalized;
        SpiderborgRb.GetComponent<Rigidbody2D>().velocity = targetDirection * (speed * 0.5f);
    }

    void AnimationController()
    {
        anim.SetInteger("SpiderState", SpiderState);

        if (target.position.y > transform.position.y)
        {
            anim.SetFloat("AnimY", 1f);
        }
        else
        {
            anim.SetFloat("AnimY", -1f);
        }
    }

    IEnumerator Landing()
    {
        occupied = true;
        anim.SetInteger("SpiderState", 5);
        yield return new WaitForSeconds(2f);
        occupied = false;
        landed = true;
        yield return null;
    }

    IEnumerator ATK1()
    {
        if (occupied == false)
        {
            occupied = true;
            yield return new WaitForSeconds(timebtwatk);
            occupied = false;
            yield return null;
        }
    }

    IEnumerator ATK2()
    {
        if (occupied == false)
        {
            occupied = true;
            lazerAtk.lazerActivated = true;
            yield return new WaitForSeconds(timebtwatk);
            occupied = false;
            yield return null;
        }
    }

    IEnumerator ATK3()
    {
        if(occupied == false)
        {
            occupied = true;
            //Debug.Log("je génère pleins d'attaques");
            shield.SetActive(true);
            yield return new WaitForSeconds(0.4f);
            Attack3();
            yield return new WaitForSeconds(timebtwatk);
            shield.SetActive(false);
            occupied = false;
            yield return null;
        }
    }

    #region Attack3
    void Attack3()
    {
        Vector2 center = transform.position;
        for (i = 0; i < numStrike1; i++)
        {
            Vector2 pos = RandomCircle(center, cercleSize);
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            Instantiate(StrikeZone, pos, Quaternion.identity);
        }
        Vector2 center2 = transform.position;
        for (j = 0; j < numStrike2; j++)
        {
            Vector2 pos2 = RandomCircle2(center2, cercleSize2);
            Quaternion rot2 = Quaternion.FromToRotation(Vector3.forward, center2 - pos2);
            Instantiate(StrikeZone, pos2, Quaternion.identity);
        }
        Vector2 center3 = transform.position;
        for (k = 0; k < numStrike3; k++)
        {
            Vector2 pos3 = RandomCircle3(center3, cercleSize3);
            Quaternion rot3 = Quaternion.FromToRotation(Vector3.forward, center3 - pos3);
            Instantiate(StrikeZone, pos3, Quaternion.identity);
        }
    }
    #endregion

    void PresetAtk3()
    {
        if (Small)
        {
            numStrike1 = 7;
            numStrike2 = 0;
            numStrike3 = 0;
            rangeAngle1 = 100;
            rangeAngle2 = 30;
            rangeAngle3 = 20;
            cercleSize = 0.8f;
        }
        else if (Medium)
        {
            numStrike1 = 7;
            numStrike2 = 12;
            numStrike3 = 0;
            rangeAngle1 = 100;
            rangeAngle2 = 30;
            rangeAngle3 = 20;
            cercleSize = 0.8f;
        }
        else if (Big)
        {
            numStrike1 = 7;
            numStrike2 = 12;
            numStrike3 = 20;
            rangeAngle1 = 100;
            rangeAngle2 = 30;
            rangeAngle3 = 20;
            cercleSize = 0.8f;
        }
        cercleSize2 = cercleSize * 2;
        cercleSize3 = cercleSize * 3;
    }

    #region Circles State 3
    Vector2 RandomCircle(Vector2 center, float radius)
    {
        float angle1 = i * rangeAngle1;
        Vector2 pos;
        pos.x = center.x + radius * Mathf.Sin(angle1 * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(angle1 * Mathf.Deg2Rad);
        return pos;
    }
    Vector2 RandomCircle2(Vector2 center, float radius)
    {
        float angle2 = j * rangeAngle2;
        Vector2 pos;
        pos.x = center.x + radius * Mathf.Sin(angle2 * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(angle2 * Mathf.Deg2Rad);
        return pos;
    }
    Vector2 RandomCircle3(Vector2 center, float radius)
    {
        float angle3 = k * rangeAngle3;
        Vector2 pos;
        pos.x = center.x + radius * Mathf.Sin(angle3 * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(angle3 * Mathf.Deg2Rad);
        return pos;
    }
    #endregion

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackdist);
    }

    #region Jump Attack

    void jumpUpdate()
    {
        if (Time.time > timeToJump)
        {
            timeToJump = Time.time + 1 / jumpRate;
            jumpPos();
        }
        if (canJump)
        {
            StartCoroutine(Jump());
        }
    }

    void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmoPos = (Mathf.Pow(1 - t, 2) * cp[0].position) + (2 * cp[1].position * (1 - t) * t) + (cp[2].position * Mathf.Pow(t, 2));
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!isAttacking && canDamage)
            {
                StartCoroutine(Damage());
            }
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!isAttacking && canDamage)
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


