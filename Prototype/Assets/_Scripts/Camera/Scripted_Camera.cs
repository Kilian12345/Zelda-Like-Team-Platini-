using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Scripted_Camera : MonoBehaviour
{
    FeedBack_Manager Fb_Mana;
    Player plScript;
    CinemachineVirtualCamera vcam;

    [SerializeField] bool OnCollision, OnTrigger, OnEvent;

    bool Triggered;

    public bool everyEventDone = false;

    void Start()
    {
        Fb_Mana = GetComponentInParent<FeedBack_Manager>();
        plScript = FindObjectOfType<Player>();
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if (Triggered == true) { StartCoroutine(CameraSwitch()); }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (OnTrigger == true)
        {

            if (collision.tag == "Player")
            {Triggered = true;}
            else
            { Triggered = false; }

        }
    }

    void Event()
    {

    }

    IEnumerator CameraSwitch()
    {
        if (everyEventDone == false)
        {
            vcam.Priority = 20;
            Fb_Mana.Scripted_Scene = true;
        }
        else
        {
            Triggered = false;
            vcam.Priority = 9;
            Fb_Mana.Scripted_Scene = false;
        }

        yield return new WaitForSeconds(5f);
        everyEventDone = true;



    }
}
