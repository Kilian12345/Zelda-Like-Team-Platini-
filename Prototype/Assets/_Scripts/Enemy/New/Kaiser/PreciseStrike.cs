using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreciseStrike : MonoBehaviour
{
    public bool Enabled;
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
    private bool isShooting;

    Player plScript;
    Animator anim;

    void Start()
    {
        plScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (Enabled)
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
            if (isShooting)
            {
                anim.SetInteger("AttackType", 2);
            }
            else
            {
                anim.SetInteger("AttackType", 0);
            }
        }
        
    }

    void coolDown()
    {
        ctrBullet = 0;
    }

    IEnumerator Shoot()
    {
        isShooting = true;
        pauseFire = false;
        yield return new WaitForSeconds(1f);
        Vector2 dir = (plScript.centrePoint.transform.position - shootPoint.transform.position).normalized;
        GameObject bullet = Instantiate(bull, shootPoint.transform.position, Quaternion.identity);
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), bullet.GetComponent<CircleCollider2D>());
        bullet.GetComponent<Rigidbody2D>().velocity = dir * bulSpeed;
        //yield return new WaitForSeconds(1f);
        //yield return new WaitForEndOfFrame();
        isShooting = false;
        StopCoroutine(Shoot());
    }

}
