﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catch : MonoBehaviour
{
    public bool isStuck;
    public float throwForce, moveHor, moveVer, sprintMultiplier;
    public GameObject coll;


    void FixedUpdate()
    {
        moveHor = Input.GetAxis("Horizontal");
        moveVer = Input.GetAxis("Vertical");
        if (coll != null)
        {
            if (Vector2.Distance(transform.position, coll.transform.position) < 2)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    coll.gameObject.transform.parent = gameObject.transform;
                    isStuck = true;
                }
                if (Input.GetButtonDown("Fire1"))
                {
                    coll.gameObject.GetComponent<Rigidbody2D>().velocity=(new Vector2(moveHor*throwForce, moveVer*throwForce));
                    coll.gameObject.transform.parent = null;
                    Invoke("isNotStuck", 0.05f);

                }
            }
            else
            {
                coll.gameObject.transform.parent = null;
            }
            if (isStuck)
            {
                coll.gameObject.transform.position = transform.position;
                //coll.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            }
            else
            {
                coll.gameObject.GetComponent<CircleCollider2D>().enabled = true;
            }
        }

    }

    void isNotStuck()
    {
        isStuck = false;
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Pickup")
        {
            coll = col.gameObject;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Pickup")
        {
            coll = col.gameObject;
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Pickup" && !isStuck)
        {
            coll.gameObject.transform.parent = null;
            coll = null;
        }
    }
}
