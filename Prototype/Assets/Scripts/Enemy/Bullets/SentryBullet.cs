using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryBullet : MonoBehaviour 
{
	public GameObject pl;
    public float speed;
	Rigidbody2D rb;
	// Use this for initialization
	void Start ()
	{
        pl = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody2D> ();
		rb.velocity = new Vector2 (-transform.position.x + pl.transform.position.x, -transform.position.y + pl.transform.position.y).normalized*speed;
        //Destroy(rb.gameObject, 5f);

    }
	// Update is called once per frame
	void Update () 
	{
		

	}
    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject, 0f);
        /*if (col.gameObject.tag == "Platform")
        {
            Destroy(rb.gameObject, 0f);
        }
        if (col.gameObject.tag == "Player")
        {
            Destroy(rb.gameObject, 0f);
        }*/
    }
}
	