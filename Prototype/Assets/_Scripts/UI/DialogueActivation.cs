using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Number of sentences and columns alowed
[System.Serializable]
public class Dialogue
{
    [HideInInspector]
    public string[] sentences;

    public string name;

    [TextArea(4, 10)]
    public string[] sentencesEN;

    [TextArea(4, 10)]
    public string[] sentencesFR;

}

public class DialogueActivation : MonoBehaviour
{
    [SerializeField] GameObject canvasIntDialogue;
    [SerializeField] AudioSource dialogueAudio;
    [SerializeField] AudioClip kuru, kaiser, spiderborg, pnj1, pnj2, bonezonk, holokaiser;
    public Animator anim;

    public int ChooseCharacter;
    public Dialogue dialogue;
    public MenuManager mn;
    public DialogueManager dialogueManager;
    public bool launch;
    Scripted_Camera camScript;

    public bool DialogueActive = false;
    public bool InsideTriggerZone = false;
    bool activateOnce;

    private void Start()
    {
        camScript = GetComponentInParent<Scripted_Camera>();
    }

    void FixedUpdate()
    {
        StateSwitch();
        Effect();

        if (mn.English == true)
        {
            //Debug.Log("EN");
            dialogue.sentences = dialogue.sentencesEN;
        }
        else if (mn.English == false)
        {
            //Debug.Log("FR");
            dialogue.sentences = dialogue.sentencesFR;
        }
    }

    void Effect()
    {
        if ((camScript.Triggered == true || launch == true) && activateOnce == false && camScript.everyEventDone == false)
        {   
            Debug.Log ("pute");
            dialogueManager.activator = this;
            dialogueManager.sentences = new Queue<string>(dialogue.sentences);
            DialogueActive = true;
            activateOnce = true;
        }
        else if (dialogueManager.DialogueCheck == false)
        {
            DialogueActive = false;
            dialogueManager.dialogueOpened = false;
            activateOnce = false;
        }
    }


    void StateSwitch()
    {
        switch (ChooseCharacter)
        {
            case 0:
                anim.SetInteger("AnimChara", 0);
                dialogueAudio.clip = pnj1;
                break;
            case 1:
                anim.SetInteger("AnimChara", 1);
                dialogueAudio.clip = kuru;
                break;
            case 2:
                anim.SetInteger("AnimChara", 2);
                dialogueAudio.clip = bonezonk;
                break;
            case 3:
                anim.SetInteger("AnimChara", 3);
                dialogueAudio.clip = holokaiser;
                break;
            case 4:
                anim.SetInteger("AnimChara", 4);
                dialogueAudio.clip = kaiser;
                break;
        }
    }
}