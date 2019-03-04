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


    public DialogueManager dialogueManager;

    public bool DialogueActive = false;
    public bool InsideTriggerZone = false;

    //Show "Press Y" int to the screen while in the trigger zone
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("activate canvas");
        canvasIntDialogue.SetActive(true);
        InsideTriggerZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("deactivate canvas");
        canvasIntDialogue.SetActive(false);
        InsideTriggerZone = false;
    }
}