using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public GameObject pl;
    //public GameObject area;
    public float bulSpeed;
    public float multiplier;
    Rigidbody2D rb;
    public bool goUp;

    // Use this for initializatio
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().centrePoint;
        multiplier = Mathf.Sqrt(Mathf.Abs(pl.transform.position.x - transform.position.x) * 9.8f / 2);
        if (pl.transform.position.x > transform.position.x)
        {
            rb.velocity = new Vector2(bulSpeed * multiplier, bulSpeed * multiplier);
        }
        else if (pl.transform.position.x < transform.position.x)
        {
            rb.velocity = new Vector2(-bulSpeed * multiplier, bulSpeed * multiplier);
        }
        if (pl.transform.position.y > transform.position.y)
        {
            goUp = true;
        }
        else if (pl.transform.position.y < transform.position.y)
        {
            goUp = false;
        }
    }
    void FixedUpdate()
    {
        //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Enemy").GetComponent<Collider2D>());

        if (goUp)
        {
            rb.AddForce(new Vector2(0, -9.8f), ForceMode2D.Force);
        }
        else
        {
            rb.AddForce(new Vector2(0, -9.8f), ForceMode2D.Force);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Enemy")
        {
            Destroy(gameObject, 0);
        }

    }
}
