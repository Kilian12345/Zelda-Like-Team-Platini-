using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player")
        {
            Destroy(gameObject, 0f);
        }
    }
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player")
        {

        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {

        }
    }
}