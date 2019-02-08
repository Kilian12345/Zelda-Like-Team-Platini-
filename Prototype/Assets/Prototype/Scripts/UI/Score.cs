using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public Text scoreDisplay;
    Player ps;

    void Start()
    {
        scoreDisplay = gameObject.GetComponent<Text>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        scoreDisplay.text = ((int)ps.PlayerScore).ToString();    
    }
}
