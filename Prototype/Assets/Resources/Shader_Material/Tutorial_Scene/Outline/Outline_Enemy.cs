using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline_Enemy : MonoBehaviour
{
    [SerializeField]
    Renderer myRenderer;
    [SerializeField] Color color;

    // Start is called before the first frame update
    void Start()
    {
       
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            color = myRenderer.material.GetColor("_ColorOutline");
            color.a = 255;
            myRenderer.material.SetColor("_ColorOutline", color);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            color = myRenderer.material.GetColor("_ColorOutline");
            color.a = 0;
            myRenderer.material.SetColor("_ColorOutline", color);

        }
    }
}
