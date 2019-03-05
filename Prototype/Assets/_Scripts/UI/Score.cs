using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI scoreDisplay;
    Player ps;


    void Start()
    {
        scoreDisplay = gameObject.GetComponent<TextMeshProUGUI>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        Debug.Log(scoreDisplay);
        scoreDisplay.text = ((int)ps.PlayerScore).ToString();

    }
}
