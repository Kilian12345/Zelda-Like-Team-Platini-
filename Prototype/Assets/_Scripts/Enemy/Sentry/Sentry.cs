﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentry : MonoBehaviour
{
    public AudioSource sentryAudio;
    public AudioClip shootSound;
    public GameObject hitbox;
    public GameObject body;
    public GameObject gun;
    public GameObject bull;
    public Transform shootPoint;
    public Transform pl;

    public float angle;
    public float FireRate = 0.5f;
    float timeToFire = 0;

    SentryHitBox st;

    public float LocalX;

    // Use this for initialization
    void Start()
    {
        sentryAudio = gameObject.GetComponent<AudioSource>();
        sentryAudio.clip = shootSound;
        st = hitbox.GetComponent<SentryHitBox>();
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        LocalX = body.transform.localScale.x;

    }

    // Update is called once per frame
    void Update()
    {
        if (pl != null)
        {
            look();
            if (st.isInside)
            {
                if (Time.time > timeToFire)
                {
                    timeToFire = Time.time + 1 / FireRate;
                    shoot();
                }
            }
        }
    }
    void look()
    {
        if (pl != null)
        {
            if (st.isInside)
            {
                Vector3 dir = (pl.position - transform.position);
                angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                //transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
                //body.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                if (pl.position.x > transform.position.x)
                {
                    body.transform.localScale = new Vector3(LocalX, body.transform.localScale.y, body.transform.localScale.z);
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
                else
                {
                    body.transform.localScale = new Vector3(-LocalX, body.transform.localScale.y, body.transform.localScale.z);
                    transform.rotation = Quaternion.AngleAxis(angle-180, Vector3.forward);
                }
            }
        }
    }
    void shoot()
    {
        sentryAudio.Play();
        Instantiate(bull, shootPoint.position, gameObject.transform.rotation);
    }


    void particleEffect()
    {

    }
}