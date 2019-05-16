using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderborgBehaviour : MonoBehaviour
{
    #region Variables
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

    void Start()
    {
        plScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        lazerAtk = GetComponentInChildren<Fow_Parent>();
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
        if ((Vector2.Distance(transform.position, target.position) > attackdist) && occupied == false)
        {
            Debug.Log("0");
            SpiderState = 0;
        }
        else if (eh.health >= 400 && occupied == false)
        {
            Debug.Log("1");
            SpiderState = 1;
            attackdist = 1f;
        }
        else if (eh.health < 400 && eh.health >= 200 && occupied == false)
        {
            Debug.Log("2");
            SpiderState = 2;

            attackdist = 1f;
        }
        else if (eh.health < 200 && eh.health >= 1 && occupied == false)
        {
            Debug.Log("3");
            SpiderState = 3;
            attackdist = 0.7f;
        }
        else if (eh.health < 1)
        {
            SpiderState = 4;
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
                StartCoroutine("ATK1");
                break;
            case 2:
                StartCoroutine("ATK2");
                break;
            case 3:
                StartCoroutine("ATK3");
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
        Debug.Log("landing");
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
}


