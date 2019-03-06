using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryHitBox : MonoBehaviour 
{
	public bool isInside;
    public float stopingDistance;
    public bool isChargingType;

    Transform pl;
    EnemyAiRange path;

    void Start()
    {
        if (!isChargingType)
        {
            path = GetComponentInParent<EnemyAiRange>();
            path.enabled = false;
        }
        pl = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
            if (Vector2.Distance(transform.position, pl.position) > stopingDistance)
            {
                isInside = true;
                if (!isChargingType)
                {
                    path.enabled = true;
                }
            }
            else
            {
                isInside = false;
                if (!isChargingType)
                {
                    path.enabled = false;
                }
            }
                
		}
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
            if (Vector2.Distance(transform.position, pl.position) > stopingDistance)
            {
                isInside = true;
                if (!isChargingType)
                {
                    path.enabled = true;
                }
            }
            else
            {
                isInside = false;
                if (!isChargingType)
                {
                    path.enabled = false;
                }
            }
        }
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			isInside=false;
            if (!isChargingType)
            {
                path.enabled = false;
            }
        }
	}

}
