using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHell : MonoBehaviour
{
    public bool Enabled;
    public int numberOfProjectiles;
    public float projectileSpeed;
    public float shotsPerBurst;
    public float shotRate;
    public float coolDownTime;
    public GameObject projectilePrefab;
    
    private Vector3 startPoint;
    private const float radius =1f;
    [SerializeField]
    private float curAngle;
    private float angle;
    private float timeToFire = 0;
    [SerializeField]
    private float ctrBullet;
    private bool pauseFire;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        startPoint = transform.position;
        if (Enabled)
        {
            if (ctrBullet < shotsPerBurst)
            {
                if (Time.time > timeToFire)
                {
                    timeToFire = Time.time + 1 / shotRate;
                    if (curAngle < 45)
                    {
                        curAngle += 4.3f;
                    }
                    else
                    {
                        curAngle = 0f;
                    }
                    spawnProjectiles(numberOfProjectiles, curAngle);
                    ctrBullet++;
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

    void spawnProjectiles(int numOfProj,float ang)
    {
        pauseFire = false;
        float angleStep = 360f / numOfProj;
        angle = ang;

        for (int i = 0; i < numOfProj; i++)
        {
            Debug.Log("Shot");
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
            Vector3 projectileMoveDirection = (projectileVector - startPoint).normalized * projectileSpeed;

            GameObject tmpObj = Instantiate(projectilePrefab, startPoint, Quaternion.identity);
            tmpObj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), tmpObj.GetComponent<CircleCollider2D>());

            angle += angleStep;

        }
    }
}
