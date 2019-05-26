using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreFinal : MonoBehaviour
{
    [SerializeField] Text scoreDisplay;
    Player ps;


    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        scoreDisplay.text = ((int)ps.PlayerScore).ToString();
    }
}
