using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer_Mouv : MonoBehaviour
{
    Transform referencePoint;
    Transform player;
    [SerializeField] Renderer[] rend;
    

    void Start()
    {
        referencePoint = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {   
        for (int i = 0; i < rend.Length; i++)
        {                   
            if(referencePoint.position.y > player.position.y)
            {rend[i].sortingOrder = 0;}
            else
            {rend[i].sortingOrder = 2;}
        }
    }
    
}
