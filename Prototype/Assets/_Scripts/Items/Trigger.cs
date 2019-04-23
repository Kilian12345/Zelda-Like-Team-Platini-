using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public bool isTriggered;
    public enum typeOfTrigger {Collision,Button,Destruction};
    public typeOfTrigger curType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Coll");
        if (curType == typeOfTrigger.Collision)
        {
            isTriggered = true;
        }
        if (curType == typeOfTrigger.Button)
        {
            if (Input.GetButtonDown("Jump"))
            {
                isTriggered = true;
            }
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("CollStay");
        if (curType == typeOfTrigger.Collision)
        {
            isTriggered = true;
        }
        if (curType == typeOfTrigger.Button)
        {
            if (Input.GetButtonDown("Jump"))
            {
                isTriggered = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("CollExit");
        if (curType == typeOfTrigger.Collision)
        {
            isTriggered = false;
        }
    }
}
