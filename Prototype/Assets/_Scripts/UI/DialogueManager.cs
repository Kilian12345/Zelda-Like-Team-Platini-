using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject canvasDialogue;
    [SerializeField] AudioSource dialogueAudio;
    public Text nameText;
    public Text dialogueText;
    public Animator animator;

    public bool DialogueCheck = false;
    [HideInInspector] public bool dialogueOpened;

    public DialogueActivation activator;

    public Queue<string> sentences;

    void Update()
    {
        //Activate Dialogue combat when pressing Y while in the Trigger Zone and dialogue is not active
        if (/* (Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.X)) &&*/ (activator.DialogueActive == true) && (dialogueOpened == false))
        {
            canvasDialogue.SetActive(true);
            StartDialogue(activator.dialogue);
            Debug.Log("activate dialogue");
            DisplayNextSentence();
            dialogueOpened = true;
        }

        // Continue to next sentence when dialogue is already active
        if ((Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.X)) && activator.DialogueActive == true)
        {
            DisplayNextSentence();
            Debug.Log("Next sentence display");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            animator.SetBool("IsOpen", false);
        }
    }

    // Starts the Dialogue
    public void StartDialogue (Dialogue dialogue)
    {
        DialogueCheck = true;

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
        if (sentences.Count > 0)
        {
            dialogueAudio.Play();
        }
        if (sentences.Count == 0)
        {
            EndDialogue();
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
        DialogueCheck = false;
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