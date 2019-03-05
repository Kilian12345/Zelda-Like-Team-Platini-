using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryHitBox : MonoBehaviour 
{
	public bool isInside;
    
    EnemyAiRange path;

    void Start()
    {
        path = GetComponentInParent<EnemyAiRange>();
        path.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			isInside=true;
            path.enabled = true;
		}
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			isInside=true;
           path.enabled = true;
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
