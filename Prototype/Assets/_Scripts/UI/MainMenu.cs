using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public AudioClip butonclick;
    public AudioSource playerAudio;

    public void PlayGame(string scene)
   {
        SceneManager.LoadScene(scene);
   }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void Sound()
    {
        playerAudio.clip = butonclick;
        playerAudio.Play();
    }
}
