using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public Transform[] cp;

    public float jumpSpeed;
    public float jumpHeightModifier;
    public float jumpRate;
    public float jumpRange;

    private Transform playerPos;

    private Vector2 curPos;
    private Vector2 gizmoPos;
    private Vector2[] points;

    public bool canJump;
    public bool isJumping;

    private float timeToJump;
    private float timeparam;

    Player plScript;

    void Start()
    {
        cp[0].position = transform.position;
        plScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerPos = plScript.centrePoint.transform;
    }

    void FixedUpdate()
    {

        if (Vector2.Distance(transform.position, plScript.centrePoint.transform.position) <= jumpRange)
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
            /*if (Vector2.Distance(transform.position, cp[2].position) <= 0.01)
            {
                isJumping = false;
                canJump = false;
            }*/
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, jumpRange);
    }

    IEnumerator Jump()
    {
        timeparam = 0;
        //isJumping = true;
        canJump = false;

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

        //timeparam = 0;
        //canJump = true;
    }

    void jumpPos()
    {
        cp[0].position = transform.position;
        cp[2].position = playerPos.position;
        cp[1].position = new Vector2((cp[0].position.x + cp[2].position.x) / 2, ((cp[0].position.y + cp[2].position.y) / 2) + jumpHeightModifier * (Vector2.Distance(cp[0].position, cp[2].position) / jumpRange));
        /*Debug.Log("Distance "+Vector2.Distance(cp[0].position, cp[2].position));
        Debug.Log("Fraction " + (Vector2.Distance(cp[0].position, cp[2].position)/jumpRange));
        Debug.Log("JumpPower " + (jumpHeightModifier * (Vector2.Distance(cp[0].position, cp[2].position) / jumpRange)));*/

        canJump = true;
    }


}
