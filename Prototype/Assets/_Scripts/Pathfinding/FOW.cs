﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOW : MonoBehaviour
{

    /// <summary>
    /// ///////////////////////GOOD
    /// </summary>
    /// 
    Player ps;
    Transform playerPos;

    public float viewRadius = 5;
    public float viewAngle = 135;
    public float stopingDistance;
    public LayerMask obstacleMask;
    Collider2D[] playerInRadius;
    public List<Transform> visiblePlayer = new List<Transform>();
    public bool PlayerDetected = false;
    public Animator anim;

    void Start()
    {

        anim = gameObject.GetComponent<Animator>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        stopingDistance = 1.5f;
        viewRadius = 10;
    }

    void FixedUpdate()
    {
        anim.SetBool("isSeen", PlayerDetected);
        FindVisiblePlayer();

    }

    void FindVisiblePlayer()
    {
        playerInRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, LayerMask.GetMask("Player"));
        visiblePlayer.Clear();

        for (int i = 0; i < playerInRadius.Length; i++)
        {
            Transform player = playerInRadius[i].transform;
            //Transform tagObjectif = GameObject.FindWithTag("Player").transform;
            Vector2 dirPlayer = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);

            Debug.DrawLine(player.transform.position, transform.position, Color.red);

            if (Vector2.Angle(dirPlayer, transform.right) < viewAngle / 2)
            {
                float distancePlayer = Vector2.Distance(transform.position, player.position);

                if (!Physics2D.Raycast(transform.position, dirPlayer, distancePlayer, obstacleMask))
                {
                    visiblePlayer.Add(player);
                    if (Vector2.Distance(transform.position, playerPos.position) > stopingDistance && Vector2.Distance(transform.position, playerPos.position) < viewRadius)
                    {
                        if (ps.EnemiesFollowing < 3)
                        {
                            PlayerDetected = true;

                        }
                    }
                    else
                    {
                        PlayerDetected = false;

                    }
                }
            }
        }

    }


    public Vector2 DirFromAngle(float angleDeg, bool global)
    {
        if (!global)
        {
            angleDeg += transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Cos(angleDeg * Mathf.Deg2Rad), Mathf.Sin(angleDeg * Mathf.Deg2Rad));
    }

}
