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
        if (Input.GetButtonDown("Cancel") && GameIsPaused == false)
        {
            Debug.Log("activate canvas");
            canvas.SetActive(true);
            GameIsPaused = true;
            StartCoroutine("Pause");
        }

        else if(Input.GetButtonDown("Cancel") && GameIsPaused == true)
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

    public void BackToMenu(string menu)
    {
        //SceneManager.LoadScene(1);
        SceneManager.LoadScene(menu);
        Time.timeScale = 1f;
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
        yield return null;
    }
}
