using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField] GameObject canvas;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject option;
    [SerializeField] GameObject btnmenu;
    [SerializeField] GameObject btnoption;
    private int check = 0;

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && GameIsPaused == false)
        {
            Debug.Log("activate canvas");
            canvas.SetActive(true);
            GameIsPaused = true;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(btnmenu);
            //StartCoroutine("Pause");
            Pause();
        }

        else if(Input.GetButtonDown("Cancel") && GameIsPaused == true)
        {
            Debug.Log("deactivate canvas");
            canvas.SetActive(false);
            menu.SetActive(true);
            option.SetActive(false);
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

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void EnableMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btnmenu);
    }

    public void EnableOption()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btnoption);
    }

    /*IEnumerator Pause()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
        yield return null;
    }*/
}
