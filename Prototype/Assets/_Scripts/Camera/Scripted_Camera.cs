using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Scripted_Camera : MonoBehaviour
{
    FeedBack_Manager Fb_Mana;
    PlayerController plScript;
    CinemachineVirtualCamera vcam;
    DialogueActivation dialogue;

    [SerializeField] bool OnCollision, OnTrigger, OnEvent ,playerStopMouv, camFollow;

    [HideInInspector] public bool Triggered;

    public bool everyEventDone = false;
    public float waitTime;
    float oldSpeed;


    void Start()
    {
        Fb_Mana = GetComponentInParent<FeedBack_Manager>();
        plScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        vcam = GetComponent<CinemachineVirtualCamera>();
        dialogue = GetComponentInChildren<DialogueActivation>();
        oldSpeed = plScript.speed;
    }

    void Update()
    {
        if (Triggered == true) { StartCoroutine(CameraSwitch()); }


    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (OnTrigger == true)
        {

            if (collision.tag == "Player")
            {Triggered = true;}
            else
            { Triggered = false; }

        }
    }


    IEnumerator CameraSwitch()
    {
        if (everyEventDone == false )
        {
            vcam.Priority = 20;
            Fb_Mana.Scripted_Scene = true;

            if (playerStopMouv == true)
            {plScript.speed = 0;}

            if (camFollow == true)
            {
                vcam.Follow = plScript.GetComponent<Player>().centrePoint.transform;
                vcam.LookAt = plScript.GetComponent<Player>().centrePoint.transform;
            }
        }
        else
        {
            vcam.Follow = null;
            vcam.LookAt = null;

            Triggered = false;
            vcam.Priority = 9;
            Fb_Mana.Scripted_Scene = false;
            plScript.speed = oldSpeed;
        }


        yield return new WaitForSeconds(0.05f);

        if (dialogue.DialogueActive == false) {everyEventDone = true;}


    }
}
