using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScene : MonoBehaviour
{
    Scripted_Camera scriptCam;
    [SerializeField] Player plScript;
    [SerializeField] Animator fadeImage;

    // Start is called before the first frame update
    void Start()
    {
        scriptCam = GetComponent<Scripted_Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptCam.everyEventDone == true)
        {
            StartCoroutine(fadeOut());
        }
    }

    IEnumerator fadeOut()
    {
        fadeImage.SetBool("Fade", true);
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(plScript.sceneIndex);
    }
}
