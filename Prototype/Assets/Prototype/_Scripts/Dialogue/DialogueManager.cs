using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    GameObject canvasDialogue;

    public Text nameText;
    public Text dialogueText;
    public Animator animator;

    public DialogueActivation dialogueActivation;
    public Dialogue dialogue;

    private Queue<string> sentences;

    //Stocks the sentences that are going to be displayed
    void Start()
    {
        sentences = new Queue<string>();
    }

    void Update()
    {
        //Activate Dialogue combat when pressing Y while in the Trigger Zone and dialogue is not active
        if ((Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.Y)) && (dialogueActivation.InsideTriggerZone == true) && (dialogueActivation.DialogueActive == false))
        {
            canvasDialogue.SetActive(true);
            StartDialogue(dialogue);
            Debug.Log("activate dialogue");
            dialogueActivation.DialogueActive = true;
        }

        // Continue to next sentence when dialogue is already active
        if ((Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.Y)) && dialogueActivation.DialogueActive == true)
        {
            DisplayNextSentence();
            Debug.Log("Next sentence display");
        }
    }

    // Starts the Dialogue
    public void StartDialogue (Dialogue dialogue)
    {
        Debug.Log("anim true");
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
    }

    // Displays the next sentences until their is no more left
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            dialogueActivation.DialogueActive = false;
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    // Ends dialogue
    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }

    // Letter appartition effect
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
}