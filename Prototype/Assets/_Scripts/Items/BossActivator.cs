using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    public GameObject boss;
    public GameObject bossUI;
    [SerializeField] Kaiser kaiser;
    [SerializeField] GameObject scriptedCamera;
    Scripted_Camera scriptCam1;

    bool niktamer = false;

    [SerializeField] List<Transform> bossAlive = new List<Transform>();

    void Start()
    {
        boss.GetComponentInChildren<Kaiser>().enabled = false;
        scriptCam1 = GetComponent<Scripted_Camera>();
        bossAlive.Add(boss.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptCam1.everyEventDone == true && niktamer == false)
        {
            boss.GetComponentInChildren<Kaiser>().enabled = true;
            niktamer = true;
        }

       if ( bossAlive.Contains(null))
        {
            scriptedCamera.GetComponent<Scripted_Camera>().OnEvent = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (boss)
            {
                boss.SetActive(true);
                bossUI.SetActive(true);
            }
            
        }
        
    }
}
