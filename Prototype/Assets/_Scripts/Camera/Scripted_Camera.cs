using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Scripted_Camera : MonoBehaviour
{
    FeedBack_Manager Fb_Mana;
    Player plScript;
    CinemachineVirtualCamera[] vcam; 

    void Start()
    {
        Fb_Mana = GetComponent<FeedBack_Manager>();
        plScript = FindObjectOfType<Player>();
    }

    void Update()
    {
        CameraSwitch();
    }

    void CameraSwitch()
    {
        for (int i = 0; i < vcam.Length; i++)
        {
            
        }
    }
}
