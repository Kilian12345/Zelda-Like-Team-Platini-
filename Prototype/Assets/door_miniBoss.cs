using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_miniBoss : MonoBehaviour
{
    [SerializeField] Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            anim.SetTrigger("Trigger");
        }
    }
}
