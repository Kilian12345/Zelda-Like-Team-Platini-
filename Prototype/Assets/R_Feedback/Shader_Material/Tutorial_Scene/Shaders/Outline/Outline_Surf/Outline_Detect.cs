using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline_Detect : MonoBehaviour
{
    Renderer myRenderer;
    ThrowingMechanic throwing;
    private MaterialPropertyBlock _propBlock;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        throwing = GetComponent<ThrowingMechanic>();
        _propBlock = new MaterialPropertyBlock();
    }
    void LateUpdate()
    {

        if (throwing.canBePicked == true)
        {
            myRenderer.GetPropertyBlock(_propBlock);
            _propBlock.SetFloat("_OutlineMode", 1);
            myRenderer.SetPropertyBlock(_propBlock);
        }
        else 
        {
            myRenderer.GetPropertyBlock(_propBlock);
            _propBlock.SetFloat("_OutlineMode", 0);
            myRenderer.SetPropertyBlock(_propBlock);
        }


    }

}
