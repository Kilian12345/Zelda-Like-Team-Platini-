using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : MonoBehaviour
{

    public bool Enabled;

    public float dashVelocity;
    public float dashCoolDown;
    public float dashRange;
    public float damageValue;

    public bool isInDashRange;
    public bool canDash;

    private float timeToDash;

    private bool isDashing;
    private bool isAttacking;

    Vector2 lastPos;
    Player plScript;


    void Start()
    {
        plScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, plScript.centrePoint.transform.position) <= dashRange)
        {
            isInDashRange = true;
        }
        else
        {
            isInDashRange = false;
        }


        if (Enabled)
        {
            if (isInDashRange)
            {
                if (Time.time > timeToDash)
                {
                    timeToDash = Time.time + 1 / dashCoolDown;
                    recPos();
                }
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
            }
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, dashRange);
    }

}
