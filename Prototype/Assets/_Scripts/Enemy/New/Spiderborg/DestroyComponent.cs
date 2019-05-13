using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyComponent : MonoBehaviour
{
    /*[SerializeField]
    CircleCollider2D col;*/
    [SerializeField]
    Object obj;
    /*void Start()
    {
        col = GetComponent<CircleCollider2D>();
        Invoke("DisableComponent", 0.5f);
    }*/

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Destroy(obj);
        }
    }

    /*void DisableComponent()
    {
        col.enabled = false;
    }*/
}
