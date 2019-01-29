using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public Text scoreDisplay;
    PlayerMovement pm;

    void Start()
    {
        scoreDisplay = gameObject.GetComponent<Text>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        scoreDisplay.text = ((int)pm.PlayerScore).ToString();    
    }
}
