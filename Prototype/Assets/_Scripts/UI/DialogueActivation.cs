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
    [SerializeField]
    GameObject canvasIntDialogue;

    public Dialogue dialogue;
    public MenuManager mn;
    public DialogueManager dialogueManager;

    public bool DialogueActive = false;
    public bool InsideTriggerZone = false;

    void FixedUpdate()
    {
        if (mn.English)
        {
            //Debug.Log("EN");
            dialogue.sentences = dialogue.sentencesEN;
        }
        else if (!mn.English)
        {
            //Debug.Log("FR");
            dialogue.sentences = dialogue.sentencesFR;
        }
    }

    //Show "Press Y" int to the screen while in the trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "PC" && dialogueManager.DialogueCheck == false)
        {
            dialogueManager.activator = this;
            dialogueManager.sentences = new Queue<string>(dialogue.sentences);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {   
        if(collision.gameObject.name == "PC" && dialogueManager.DialogueCheck == false)
        {
            canvasIntDialogue.SetActive(true);
            InsideTriggerZone = true;
        }
        else if (collision.gameObject.name == "PC" && dialogueManager.DialogueCheck == true)
        {
            canvasIntDialogue.SetActive(false);
            InsideTriggerZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "PC")
        {
            canvasIntDialogue.SetActive(false);
            InsideTriggerZone = false;
        }
    }
}