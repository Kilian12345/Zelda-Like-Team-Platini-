using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryBullet : MonoBehaviour 
{
	public GameObject pl;
    public float speed;
	Rigidbody2D rb;
    Collider2D co;
    // Use this for initialization




    void Start ()
	{
        pl = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody2D> ();
        co = GetComponent<Collider2D>();
        rb.velocity = new Vector2 (-transform.position.x + pl.transform.position.x, -transform.position.y + pl.transform.position.y).normalized*speed;


        Destroy(rb.gameObject, 5f);

    }
	// Update is called once per frame
	void Update () 
	{
		

	}
    void OnCollisionEnter2D(Collision2D coll)
    {

        StartCoroutine(Attach(coll));

        /*Destroy(gameObject, 0f);

        if (col.gameObject.tag == "Platform")
        {
            Destroy(rb.gameObject, 0f);
        }
        if (col.gameObject.tag == "Player")
        {
            Destroy(rb.gameObject, 0f);
        }*/
    }
     
    IEnumerator Attach(Collision2D coll)
    {
        Destroy(rb);
        Destroy(co);
        gameObject.transform.parent = coll.transform;
        yield return new WaitForSeconds(1);
        Destroy(gameObject, 0f);


    }


}
	