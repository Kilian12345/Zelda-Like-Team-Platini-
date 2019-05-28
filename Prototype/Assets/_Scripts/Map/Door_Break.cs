using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Break : MonoBehaviour
{
    FeedBack_Manager Fb_Mana;
    Player plScript;
    public ParticleSystem boxExpolsion;
    AudioSource Expl;

    private void Start()
    {
        Fb_Mana = FindObjectOfType<FeedBack_Manager>();
        plScript = FindObjectOfType<Player>();
        Expl = GameObject.FindGameObjectWithTag("DestructionAudio").GetComponent<AudioSource>();

    }

    public void Destroy()
    {
        Expl.Play();
        Fb_Mana.StartCoroutine(Fb_Mana.vibrateBrève(0.15f, 0.25f, 0.25f));
        Instantiate(boxExpolsion, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.1f);
        Fb_Mana.throwScrShake = true;
    }

}
