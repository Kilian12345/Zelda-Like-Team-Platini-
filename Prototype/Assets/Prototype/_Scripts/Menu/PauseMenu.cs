using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField]
    GameObject canvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameIsPaused == false)
        {
            Debug.Log("activate canvas");
            canvas.SetActive(true);
            GameIsPaused = true;
            Time.timeScale = 0f;
        }

        else if(Input.GetKeyDown(KeyCode.Escape) && GameIsPaused == true)
        {
            Debug.Log("deactivate canvas");
            canvas.SetActive(false);
            GameIsPaused = false;
            Time.timeScale = 1f;
        }

    }

    public void ResumeGame()
    {
        canvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

}
