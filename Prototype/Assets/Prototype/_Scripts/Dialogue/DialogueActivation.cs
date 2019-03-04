using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivation : MonoBehaviour
{
    [SerializeField]
    GameObject canvasDialogue;

    [SerializeField]
    GameObject canvasIntDialogue;

    public Dialogue dialogue;
    // public int EnemyCount = 0;

    private bool InsideTriggerZone = false;

    private void Start()
    {
        D_manager = GetComponent<DialogueManager>();

        D_manager.DisplayNextSentence();
    }

    //Show "Press Y" int to the screen while in the trigger zone
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("activate canvas");
        canvasIntDialogue.SetActive(true);
        InsideTriggerZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("activate canvas");
        canvasIntDialogue.SetActive(false);
        InsideTriggerZone = false;
    }

    // TriggerDialogue Function
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    void Update()
    {

        //Activate Dialogue combat when pressing Y while in the Trigger Zone
        if (Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.Y) && InsideTriggerZone == true)
        {
            Debug.Log("activate canvas");
            canvasDialogue.SetActive(true);
            TriggerDialogue();
        }

    }
}