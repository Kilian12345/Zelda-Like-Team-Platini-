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
    void Update()
    {
        myRenderer.GetPropertyBlock(_propBlock);

        if (throwing.canBePicked == true)
        {_propBlock.SetFloat("_OutlineMode", 1);}
        else 
        {_propBlock.SetFloat("_OutlineMode", 0);}

        myRenderer.SetPropertyBlock(_propBlock);
    }

}
