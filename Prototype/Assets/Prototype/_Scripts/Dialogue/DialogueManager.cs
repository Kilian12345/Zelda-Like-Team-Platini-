﻿using System.Collections;
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

    public bool DialogueCheck = false;

    public DialogueActivation activator;

    public Queue<string> sentences;

    void Update()
    {
        //Activate Dialogue combat when pressing Y while in the Trigger Zone and dialogue is not active
        if ((Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.Y)) && (activator.InsideTriggerZone == true) && (activator.DialogueActive == false))
        {
            canvasDialogue.SetActive(true);
            StartDialogue(activator.dialogue);
            Debug.Log("activate dialogue");
            activator.DialogueActive = true;
        }

        // Continue to next sentence when dialogue is already active
        if ((Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.Y)) && activator.DialogueActive == true)
        {
            DisplayNextSentence();
            Debug.Log("Next sentence display");
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
        if (sentences.Count == 0)
        {
            EndDialogue();
            activator.DialogueActive = false;
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