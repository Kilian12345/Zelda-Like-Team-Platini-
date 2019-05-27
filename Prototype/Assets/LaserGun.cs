using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{

    public float FireRate;
    public float bulSpeed;

    public Transform shootPoint;
    public GameObject bull;

    private float timeToFire;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeToFire)
        {
            timeToFire = Time.time + 1 / FireRate;
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.1f);
        Vector2 dir = Vector2.left;
        GameObject bullet = Instantiate(bull, shootPoint.position, Quaternion.identity);
        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), bullet.GetComponent<Collider2D>());
        bullet.GetComponent<Rigidbody2D>().velocity = dir * bulSpeed;
        yield return new WaitForSeconds(0.25f);
        StopCoroutine(Shoot());
    }
}
