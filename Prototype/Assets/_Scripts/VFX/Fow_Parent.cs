using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fow_Parent : MonoBehaviour
{

    public float viewRadius = 5;
    public float radius;
    public float viewAngle = 135;
    public LayerMask obstacleMask;
    Collider2D[] playerInRadius;
    public List<Transform> visiblePlayer = new List<Transform>();
    public bool PlayerDetected = false;

    public float DamageDeal = 1;

    void Start()
    {
        viewRadius = 0;
    }

    void FixedUpdate()
    {
        FindVisiblePlayer();
        ZoneIncrease();
    }

    void FindVisiblePlayer()
    {
       
        playerInRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, LayerMask.GetMask("Ennemy"));

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

                if (Physics2D.Raycast(transform.position, dirPlayer, distancePlayer, obstacleMask))
                {

                    visiblePlayer.Add(player);
                    PlayerDetected = true;   
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

    void ZoneIncrease()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            float puissance = (float)1.00001 ;
            viewRadius = Mathf.Clamp( ((viewRadius * puissance)* (float)1.1)  , (float)0.1, radius);  
        }
        /* else 
        {
            viewRadius = Mathf.Lerp( viewRadius, 0, Time.deltaTime*3);

            if(viewRadius <= 0.1f) {viewRadius = 0;}
        }*/
    }



}
