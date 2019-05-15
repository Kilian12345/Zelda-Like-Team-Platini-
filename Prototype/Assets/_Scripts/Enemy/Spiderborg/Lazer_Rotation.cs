using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer_Rotation : MonoBehaviour
{
    Fow_Parent fow;

    bool radiusIsGood;

    float zAxis;
    [SerializeField] float rotationSpeed = 200;


    void Start()
    {
        fow = GetComponentInChildren<Fow_Parent>();
        zAxis = transform.rotation.z;

    }


    void Update()
    {

        if (fow.viewRadius >= fow.radius) { radiusIsGood = true; }

        if (fow.lazerActivated == true && radiusIsGood == true)
        {

            zAxis += Time.deltaTime;


            transform.Rotate(0, 0, zAxis * rotationSpeed);

        }
        else
        {
            transform.Rotate(0, 0, 0);
        }

    }

}
