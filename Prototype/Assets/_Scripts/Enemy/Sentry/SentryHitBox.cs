using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryHitBox : MonoBehaviour 
{
	public bool isInside;
    public float stopingDistance;
    public bool isChargingType;

    Transform pl;

    void Start()
    {
        pl = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
            if (Vector2.Distance(transform.position, pl.position) > stopingDistance)
            {
                isInside = true;
            }
            else
            {
                isInside = false;
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
            }
            else
            {
                isInside = false;
            }
        }
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			isInside=false;
        }
	}

}
