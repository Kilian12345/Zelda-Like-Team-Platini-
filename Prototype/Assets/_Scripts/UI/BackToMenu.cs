using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene(1);
            Debug.Log("Escape key pressed");
        }
    }
}
