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
    [SerializeField] bool Priority = false;
    Scripted_Camera camScript;

    public bool DialogueActive = false;
    public bool InsideTriggerZone = false;
    bool activateOnce;

    private void Awake()
    {
        camScript = GetComponentInParent<Scripted_Camera>();
        if(Priority == true)
        {
            StateSwitch();
        }
        
    }

    void Update()
    {
        Effect();

        /*if (mn.English == true)
        {
            //Debug.Log("EN");
            dialogue.sentences = dialogue.sentencesEN;
        }
        else if (mn.English == false)
        {
            //Debug.Log("FR");
            dialogue.sentences = dialogue.sentencesFR;
        }*/
    }

    void Effect()
    {
        if ((camScript.Triggered == true) && (activateOnce == false) && (camScript.everyEventDone == false))
        {

            StateSwitch();

            Debug.Log(anim.GetInteger("AnimChara"));
            if (MenuManager.English == true)
            {
                Debug.Log("EN");
                dialogue.sentences = dialogue.sentencesEN;
            }
            if (MenuManager.English == false)
            {
                Debug.Log("FR");
                dialogue.sentences = dialogue.sentencesFR;
            }
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
            //StartCoroutine("Waitaaa");
        }
    }


    void StateSwitch()
    {
        switch (ChooseCharacter)
        {
            case 0:
                anim.SetInteger("AnimChara", 0);
                //dialogueAudio.clip = bonezonk;
                dialogueAudio.clip = kuru;
                break;
            case 1:
                anim.SetInteger("AnimChara", 1);
                //dialogueAudio.clip = bonezonk;
                dialogueAudio.clip = kuru;
                Debug.Log("img");
                break;
            case 2:
                anim.SetInteger("AnimChara", 2);
                //dialogueAudio.clip = pnj1;
                dialogueAudio.clip = kuru;
                break;
            case 3:
                anim.SetInteger("AnimChara", 3);
                //dialogueAudio.clip = pnj1;
                dialogueAudio.clip = kuru;
                break;
            case 4:
                anim.SetInteger("AnimChara", 4);
                dialogueAudio.clip = kuru;
                break;
            case 5:
                anim.SetInteger("AnimChara", 5);
                dialogueAudio.clip = kuru;
                break;
            case 6:
                anim.SetInteger("AnimChara", 6);
                //dialogueAudio.clip = pnj1;
                dialogueAudio.clip = kuru;
                break;
            case 7:
                anim.SetInteger("AnimChara", 7);
                //dialogueAudio.clip = spiderborg;
                dialogueAudio.clip = kuru;
                Debug.Log("img");
                break;
            case 8:
                anim.SetInteger("AnimChara", 8);
                //dialogueAudio.clip = holokaiser;
                dialogueAudio.clip = kaiser;
                break;
            case 9:
                anim.SetInteger("AnimChara", 9);
                //dialogueAudio.clip = holokaiser;
                dialogueAudio.clip = kaiser;
                break;
            case 10:
                anim.SetInteger("AnimChara", 10);
                dialogueAudio.clip = kaiser;
                break;
        }
    }

    /*IEnumerator Waitaaa()
    {
        yield return new WaitForSeconds(2f);
        activateOnce = false;
    }*/
}