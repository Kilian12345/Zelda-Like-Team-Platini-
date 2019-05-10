using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline_Detect : MonoBehaviour
{
    Renderer myRenderer;
    private MaterialPropertyBlock _propBlock;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        _propBlock = new MaterialPropertyBlock();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            myRenderer.GetPropertyBlock(_propBlock);
            _propBlock.SetFloat("_OutlineMode", 1);
            myRenderer.SetPropertyBlock(_propBlock);           
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            myRenderer.GetPropertyBlock(_propBlock);
            _propBlock.SetFloat("_OutlineMode", 0);
            myRenderer.SetPropertyBlock(_propBlock);

        }
    }
}
