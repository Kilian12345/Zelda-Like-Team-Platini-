using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_miniBoss : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] BoxCollider2D collider2DBarrier;
    bool blablabla = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && blablabla == false)
        {
            anim.SetBool("Trig", true);
            collider2DBarrier.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            blablabla = true;
            anim.SetBool("Trig", false);
            collider2DBarrier.enabled = true;
        }
    }
    private void Update()
    {
        gameObject.SetActive(true);
    }
}
