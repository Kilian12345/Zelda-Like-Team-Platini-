﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropChance : MonoBehaviour
{
    public GameObject CalciumPack;
    Player ps;
    GameObject player;
    private bool canSpawn;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ps = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player)
        {
            canSpawn = true;
        }
        else
        {
            canSpawn = false;
        }
    }

    /*void OnDestroy()
    {

        if (Random.Range(0, 100) > ps.curDropChanceRate)
        {
            ps.curDropChanceRate += 5;
        }
        else
        {
            Instantiate(CalciumPack, transform.position, Quaternion.identity);
            ps.curDropChanceRate = ps.DropChanceRate;
        }
        
    }*/

    private void OnDisable()
    {
        if (canSpawn)
        {
            if (Random.Range(0, 100) > ps.curDropChanceRate)
            {
                ps.curDropChanceRate += 5;
            }
            else
            {
                Instantiate(CalciumPack, transform.position, Quaternion.identity);
                ps.curDropChanceRate = ps.DropChanceRate;
            }
        }
        
    }
}