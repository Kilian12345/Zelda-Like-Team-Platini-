﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingMechanic : MonoBehaviour
{
    public float throwDistance, throwVelocity, pickupDistance;
    public float damage;
    public bool isCaught, toThrow;
    public bool canBePicked;
    public bool hasBeenThrowed;
    public bool isThrowing;
    private Vector2 lastPos;
    Player ps;
    GameObject player;
    Collider2D bColl;
    FeedBack_Manager Fb_Mana;
    public ParticleSystem boxExpolsion;


    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player = GameObject.FindGameObjectWithTag("Player");
        bColl = GetComponent<Collider2D>();
        Fb_Mana = GameObject.FindGameObjectWithTag("FeedBack_Manager").GetComponent<FeedBack_Manager>();
    }

    void FixedUpdate()
    {

        if (Vector2.Distance(transform.position, ps.centrePoint.transform.position) <= pickupDistance)
        {
            canBePicked = true;

            if (Input.GetKeyDown(KeyCode.Joystick1Button4)  /*Input.GetKeyDown(KeyCode.Space)*/)
            {
                if (!isCaught)
                {
                    isCaught = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.Joystick1Button5)  /*Input.GetKeyDown(KeyCode.A)*/)
            {
                if (isCaught)
                {
                    isCaught = false;
                    recPos();
                }
            }
            Physics2D.IgnoreCollision(bColl, player.GetComponent<CapsuleCollider2D>(), toThrow);
        }
        else { canBePicked = false; }
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        //Debug.Log(GetComponent<Rigidbody2D>().velocity);

        if (isCaught)
        {
            bColl.enabled = false;
            transform.position = ps.carryPoint.transform.position;
        }
        else
        {
            hasBeenThrowed = false;
            bColl.enabled = true;
        }

        if (toThrow)
        {
            Throw();
        }
        if (Vector2.Distance(transform.position, lastPos) <= 1)
        {
            toThrow = false;
            if (!hasBeenThrowed)
            {
                if (!isCaught)
                {
                    isThrowing = false;
                    hasBeenThrowed =true;
                    Fb_Mana.boxExpolsion = boxExpolsion;
                }
            }

        }
    }

    public void Destroy()
    {
             Instantiate(Fb_Mana.boxExpolsion, transform.position, Quaternion.identity );
             Destroy (gameObject);
             Fb_Mana.throwScrShake = true;
    }


    void recPos()
    {
        Vector3 movement = new Vector3(ps.moveHor, ps.moveVer, 0);
        lastPos = player.transform.position + movement * throwDistance;
        hasBeenThrowed=false;
        toThrow = true;
    }

    void Throw()
    {
        transform.position = Vector2.MoveTowards(transform.position, lastPos, throwVelocity * Time.deltaTime);
        isThrowing = true;
    }
}
