using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rendering_Chara : MonoBehaviour
{
    public Color Tint = new Color(1,1,1,1)
        , OpaqueColor = new Color(1,1,1,1)
        , DissolveEmission = new Color(1, 1, 1, 1)
        , OutlineColor = new Color(1, 1, 1, 1);

    public bool isOpaque, isDissolve, isOutline;

    [Range (-0.1f,1.1f)]public float dissolveAmout;
    [Range(0 , 0.5f)] public float dissolveGrain;

    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;

    void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Get the current value of the material properties in the renderer.
        _renderer.GetPropertyBlock(_propBlock);
        // Assign our new value.
        _propBlock.SetColor("_Color", Tint * 1.3f);
        _propBlock.SetColor("_OpaqueColor", OpaqueColor);

        if (isOpaque == true)
        { _propBlock.SetFloat("_OpaqueMode", 1); }
        else
        { _propBlock.SetFloat("_OpaqueMode", 0); }

        if (isDissolve == true)
        { _propBlock.SetFloat("_DissolveMode", 1);
          _propBlock.SetFloat("_DissolveAmount", dissolveAmout);
          _propBlock.SetFloat("_DissolveGrain", dissolveGrain);
          _propBlock.SetColor("_DissolveEmission", DissolveEmission);
        }
        else
        { _propBlock.SetFloat("_DissolveMode", 0); }

        if (isOutline == true)
        {
           _propBlock.SetFloat("_OutlineMode", 1);
           _propBlock.SetColor("_ColorOutline", OutlineColor);
        }
        else
        { _propBlock.SetFloat("_OpaqueMode", 0); }
        // Apply the edited values to the renderer.
        _renderer.SetPropertyBlock(_propBlock);
    }
}
