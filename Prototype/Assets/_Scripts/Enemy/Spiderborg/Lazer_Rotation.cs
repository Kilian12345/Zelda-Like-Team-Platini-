using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer_Rotation : MonoBehaviour
{
    Fow_Parent fow;
    Player plScript;
    public GameObject LaserPoint;
    bool radiusIsGood;

    [SerializeField] [Range(0,1f)] float damage;
    [SerializeField] float rotationspeed = 10;
    float rotationleft = 360;

    Collider2D coll;



    void Start()
    {
        plScript = FindObjectOfType<Player>();
        fow = GetComponentInChildren<Fow_Parent>();
        coll = GetComponent<Collider2D>();
        coll.enabled = false;
    }


    void Update()
    {
        transform.position = LaserPoint.transform.position;
        float rotation = rotationspeed * Time.deltaTime;



        transform.position = LaserPoint.transform.position;
        if (fow.viewRadius >= fow.radius) { radiusIsGood = true; }

        if (fow.lazerActivated == true && radiusIsGood == true)
        {
            coll.enabled = true;
            // StartCoroutine(Sale());
            if (rotationleft > rotation)
            {
                rotationleft -= rotation;
            }
            else
            {
                rotation = rotationleft;
                rotationleft = 0;
                if (fow.lazerActivated == true)
                {
                    coll.enabled = false;
                    StartCoroutine(Sale());
                }
                    
            }
            transform.Rotate(0, 0, rotation);
        }
    }

    IEnumerator Sale()
    {
        yield return new WaitForSeconds(1f);

        if(transform.localRotation.z < 2 && transform.localRotation.z > -10)
        {
            yield return new WaitForSeconds(0.1f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            fow.lazerActivated = false;
            rotationleft = 360f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            plScript.health += damage;
        }
    }

}
