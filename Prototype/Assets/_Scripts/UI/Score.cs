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
        scoreDisplay.text = ((int)ps.PlayerScore).ToString();
    }
}
