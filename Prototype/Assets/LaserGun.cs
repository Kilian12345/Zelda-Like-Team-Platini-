using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{

    public float FireRate;
    public float bulSpeed;
    public float shootingRange;

    public Transform[] shootPoint;
    public GameObject bull;

    private float timeToFire;

    Player plScript;
    void Start()
    {
        plScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(plScript.centrePoint.transform.position, transform.position) < shootingRange && plScript.centrePoint.transform.position.x < shootPoint[0].transform.position.x)
        {
            if (Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / FireRate;
                StartCoroutine(Shoot());
            }
        }

    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.2f);
        //Vector2 dir = Vector2.left;
        Vector2 dir = (plScript.centrePoint.transform.position - shootPoint[0].transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject bullet = Instantiate(bull, shootPoint[0].position, Quaternion.identity);
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), bullet.GetComponent<Collider2D>());
        bullet.GetComponent<Rigidbody2D>().velocity = dir * bulSpeed;
        yield return new WaitForSeconds(0.1f);

        dir = (plScript.centrePoint.transform.position - shootPoint[1].transform.position).normalized;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bullet = Instantiate(bull, shootPoint[1].position, Quaternion.identity);
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), bullet.GetComponent<Collider2D>());
        bullet.GetComponent<Rigidbody2D>().velocity = dir * bulSpeed;
        yield return new WaitForSeconds(0.2f);

        dir = (plScript.centrePoint.transform.position - shootPoint[2].transform.position).normalized;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bullet = Instantiate(bull, shootPoint[2].position, Quaternion.identity);
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), bullet.GetComponent<Collider2D>());
        bullet.GetComponent<Rigidbody2D>().velocity = dir * bulSpeed;


        StopCoroutine(Shoot());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
