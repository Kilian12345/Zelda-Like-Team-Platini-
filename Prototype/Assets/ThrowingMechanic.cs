using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingMechanic : MonoBehaviour
{
    public float throwDistance,throwVelocity,pickupDistance;
    public bool isCaught,toThrow;
    private Vector2 lastPos;
    Player ps;
    GameObject player;
    BoxCollider2D bColl;


    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player = GameObject.FindGameObjectWithTag("Player");
        bColl = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= pickupDistance)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button4))
            {
                Debug.Log("ASD");
                if (!isCaught)
                {
                    isCaught = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                Debug.Log("LSD");
                if (isCaught)
                {
                    isCaught = false;
                    recPos();
                }
            }
            Physics2D.IgnoreCollision(bColl, player.GetComponent<CapsuleCollider2D>(),toThrow);
        }
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        //Debug.Log(GetComponent<Rigidbody2D>().velocity);

        if (isCaught)
        {
            bColl.enabled = false;
            transform.position = ps.carryPoint.transform.position;
        }
        else
        { 
            bColl.enabled = true;
        }

        if (toThrow)
        {
            Throw();
        }
        if (Vector2.Distance(transform.position, lastPos) <= 1)
        {
            toThrow = false;
        }
    }

    void recPos()
    {
        Vector3 movement = new Vector3(ps.moveHor, ps.moveVer,0);
        lastPos = player.transform.position+movement*throwDistance;
        toThrow = true;
    }

    void Throw()
    {
        transform.position = Vector2.MoveTowards(transform.position, lastPos, throwVelocity * Time.deltaTime);
    }
}
