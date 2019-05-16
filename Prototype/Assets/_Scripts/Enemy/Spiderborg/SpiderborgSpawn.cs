using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderborgSpawn : MonoBehaviour
{
    SpiderborgBehaviour sbs;
    public GameObject entity;
    bool spawned = false;
    public GameObject obj;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player") && spawned == false)
        {
            entity.SetActive(true);
            spawned = true;
        }
        /*if (sbs.SpiderState == 4)
        {
            Destroy(obj);
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player") && spawned == false)
        {
            entity.SetActive(true);
            spawned = true;
        }
        /*if (sbs.SpiderState == 4)
        {
            Destroy(obj);
        }*/
    }

}
