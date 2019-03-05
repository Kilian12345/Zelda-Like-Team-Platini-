using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Number of sentences and columns alowed
[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(4, 10)]
    public string[] sentences;
}

public class DialogueActivation : MonoBehaviour
{
    [SerializeField]
    GameObject canvasIntDialogue;

    public Dialogue dialogue;

    public DialogueManager dialogueManager;

    public bool DialogueActive = false;
    public bool InsideTriggerZone = false;

    //Show "Press Y" int to the screen while in the trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "PC")
        {
            Debug.Log("ACESS");
            dialogueManager.activator = this;
            dialogueManager.sentences = new Queue<string>(dialogue.sentences);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {   
        if(collision.gameObject.name == "PC")
        {
            Debug.Log("activate canvas");
            canvasIntDialogue.SetActive(true);
            InsideTriggerZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "PC")
        {
            Debug.Log("deactivate canvas");
            canvasIntDialogue.SetActive(false);
            InsideTriggerZone = false;
        }
    }
}