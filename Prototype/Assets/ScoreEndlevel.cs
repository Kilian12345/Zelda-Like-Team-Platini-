using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEndlevel : MonoBehaviour
{

    [SerializeField] GameObject ScoreTotal;
    [SerializeField] Player pl;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            ScoreTotal.SetActive(true);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            ScoreTotal.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            ScoreTotal.SetActive(false);
        }
    }
}
