﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOW : MonoBehaviour
{


    public float viewRadius = 5;
    public float viewAngle = 135;
    public LayerMask obstacleMask, playerMask;
    Collider2D[] playerInRadius;
    public List<Transform> visiblePlayer = new List<Transform>();
    public bool PlayerDetected = false;

    
    void FixedUpdate()
    {

        FindVisiblePlayer();
         
    }

    void FindVisiblePlayer()
    {

        playerInRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, LayerMask.GetMask("Player"));

        visiblePlayer.Clear();
        PlayerDetected = false;
        viewRadius = 1;

        for (int i = 0; i < playerInRadius.Length; i++)
        {
            Transform player = playerInRadius[i].transform;
            //Transform tagObjectif = GameObject.FindWithTag("Player").transform;
            Vector2 dirPlayer = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);

            Debug.DrawLine(player.transform.position, transform.position, Color.red);

            if (Vector2.Angle(dirPlayer, transform.right) < viewAngle / 2)
            {
                float distancePlayer = Vector2.Distance(transform.position, player.position);

                if(!Physics2D.Raycast(transform.position, dirPlayer, distancePlayer, obstacleMask))
                {

                    visiblePlayer.Add(player);
                    PlayerDetected = true;
                    viewRadius = 10;
                    

                }  

                
            }

         
        }


    }



    public Vector2 DirFromAngle (float angleDeg, bool global)
    {
        if(!global)
        {
            angleDeg += transform.eulerAngles.z;

        }
        return new Vector2(Mathf.Cos(angleDeg * Mathf.Deg2Rad), Mathf.Sin(angleDeg * Mathf.Deg2Rad));




    }



}
