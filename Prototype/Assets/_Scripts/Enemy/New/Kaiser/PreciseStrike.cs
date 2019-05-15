using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreciseStrike : MonoBehaviour
{
    public bool preciseShotEnabled;
    public float bulSpeed;
    public float shotsPerBurst;
    public float shotRate;
    public float coolDownTime;
    public GameObject bull;
    public GameObject shootPoint;

    private float timeToFire = 0;
    [SerializeField]
    private float ctrBullet;
    private bool pauseFire;

    Player plScript;

    void Start()
    {
        plScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void FixedUpdate()
    {
        if (preciseShotEnabled)
        {
            if (ctrBullet < shotsPerBurst)
            {
                if (Time.time > timeToFire)
                {
                    ctrBullet++;
                    timeToFire = Time.time + 1 / shotRate;
                    StartCoroutine(Shoot());
                }
            }
            else
            {
                if (!pauseFire)
                {
                    pauseFire = true;
                    Invoke("coolDown", coolDownTime);
                }
                
            }
        }
    }

    void coolDown()
    {
        ctrBullet = 0;
    }

    IEnumerator Shoot()
    {
        pauseFire = false;
        Vector2 dir = (plScript.centrePoint.transform.position - transform.position).normalized;
        GameObject bullet = Instantiate(bull, shootPoint.transform.position, Quaternion.identity);
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), bullet.GetComponent<CircleCollider2D>());
        bullet.GetComponent<Rigidbody2D>().velocity = dir * bulSpeed;
        yield return new WaitForEndOfFrame();
        StopCoroutine(Shoot());
    }

}
