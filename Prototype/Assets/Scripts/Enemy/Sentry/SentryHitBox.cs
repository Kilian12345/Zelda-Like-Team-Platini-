using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryHitBox : MonoBehaviour 
{
	public bool isInside;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			isInside=true;
		}
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			isInside=true;
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
