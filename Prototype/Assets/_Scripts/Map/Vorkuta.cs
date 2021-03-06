﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vorkuta : MonoBehaviour
{
    FeedBack_Manager Fb_Mana;
    Animator anim;
    Collider2D coll;
    SpriteRenderer rend;
    bool shake = true;
    public GameObject towerlight;

    private void Start()
    {
        Fb_Mana = FindObjectOfType <FeedBack_Manager>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Throwing_Object" && col.gameObject.GetComponent<ThrowingMechanic>().isThrowing)
        {
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Throwing_BIG" && col.gameObject.GetComponent<ThrowingMechanic>().isThrowing)
        {
            anim.SetBool("DestroyTower", true);
            coll.enabled = false;
            towerlight.SetActive(false);
            Fb_Mana.throwScrShake = true;
            Fb_Mana.StartCoroutine(Fb_Mana.vibrateBrève(1.5f, 0.7f, 0.5f));
            StartCoroutine(Shake());
            Instantiate(Fb_Mana.boxExpolsion, col.transform.position, Quaternion.identity);
            Destroy(col.gameObject);
        }

    }

    private void Update()
    {
        if (shake == false)
        {
            Fb_Mana.shakeAmplitude = 0.5f;
            Fb_Mana.shakeFrequency = 0.3f;
            Fb_Mana.CameraShake();
        }
    }

    IEnumerator Shake()
    {
        shake = false;
        yield return new WaitForSeconds(1.5f);
        shake = true;
    }

    void Layer()
    {
        rend.sortingOrder = 0;
        Fb_Mana.StopCoroutine(Fb_Mana.vibrateBrève(2, 0.7f, 0.0f));
    }
}
