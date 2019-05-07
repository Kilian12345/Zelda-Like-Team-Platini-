using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalciumBones : MonoBehaviour
{
    public float CalciumRefill;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            Destroy(gameObject, 10f);
        }
        else
        {
            Destroy(gameObject);
        }
                
    }
}
