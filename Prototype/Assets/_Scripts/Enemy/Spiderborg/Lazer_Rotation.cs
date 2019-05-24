using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer_Rotation : MonoBehaviour
{
    Fow_Parent fow;
    public GameObject LaserPoint;
    bool radiusIsGood;

    [SerializeField] float rotationspeed = 10;
    float rotationleft = 360;



    void Start()
    {
        fow = GetComponentInChildren<Fow_Parent>();
    }


    void Update()
    {
        transform.position = LaserPoint.transform.position;
        float rotation = rotationspeed * Time.deltaTime;



        transform.position = LaserPoint.transform.position;
        if (fow.viewRadius >= fow.radius) { radiusIsGood = true; }

        if (fow.lazerActivated == true && radiusIsGood == true)
        {
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

}
