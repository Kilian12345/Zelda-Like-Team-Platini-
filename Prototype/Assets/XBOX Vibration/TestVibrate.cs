using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class TestVibrate : MonoBehaviour
{
    // Start is called before the first frame update
     public float testA;
     public float testB;
     public float testC;
     public float testD;
 
    void Start()
    {
        testA = 0;
        testB = 0;
        testC = 0;
        testD = 0;
    }
     void Update()
     {        
         GamePad.SetVibration(0,testA,testB);
         GamePad.SetVibration(PlayerIndex.One,testC, testD);   

         if (Input.GetButton("Attacking")) 
         {
             testA = 0;
             testB = 0;
             testC = 1;
             testD = 1;
         }
         else
         {
             testA = 0;
             testB = 0;
             testC = 0;
             testD = 0;
         }
     }   
}
