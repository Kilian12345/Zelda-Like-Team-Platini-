using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public bool isTriggered;
    public enum typeOfTrigger {Collision,Button,Destruction};
    public typeOfTrigger curType;
    public ParticleSystem boxExpolsion;
    public Light light;


    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (curType == typeOfTrigger.Collision)
        {
            isTriggered = true;
        }
        if (curType == typeOfTrigger.Destruction)
        {
            if (Input.GetButtonDown("Jump") && !isTriggered)
            {
                isTriggered = true;
                Instantiate(boxExpolsion, transform.position, Quaternion.identity);
            }
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (curType == typeOfTrigger.Collision)
        {
            isTriggered = true;
        }
        if (curType == typeOfTrigger.Destruction)
        {
            if (Input.GetButtonDown("Jump") && !isTriggered)
            {
                isTriggered = true;
                Instantiate(boxExpolsion, transform.position, Quaternion.identity);
                light.color = new Color(0, 1, 0);
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (curType == typeOfTrigger.Collision)
        {
            isTriggered = false;
        }
    }
}
