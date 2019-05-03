﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rendering_Chara : MonoBehaviour
{
    public Color Tint = new Color(1,1,1,1)
        , HitColor = new Color(1,1,1,1)
        , DissolveColor = new Color(1,1,1,1)
        , DissolveEmission = new Color(0, 0, 0, 1)
        , OutlineColor = new Color(1, 1, 1, 1);
    
    Color HitColorTransition = Color.black;

    public bool isOpaque, isDissolve, isOutline;
    [Range(40 , 80)] public float timeHitFB;
    double Sinus;

    [Range (-0.1f,1.1f)]public float dissolveAmout;
    [Range(0 , 0.5f)] public float dissolveGrain;
    private Renderer _renderer;
    private Player plScript;
    private MaterialPropertyBlock _propBlock;

    void Awake()
    {
        plScript = FindObjectOfType<Player>();
        _propBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {

        _renderer.GetPropertyBlock(_propBlock);
        // Assign our new value.
        _propBlock.SetColor("_Color", Tint * 1.3f);

         if (isOpaque == true )
         {Sinus = Mathf.Sin(Time.time*timeHitFB) * 1.2; StartCoroutine(HitTime());}
         else
         { _propBlock.SetFloat("_OpaqueMode", 0); }



        if (isDissolve == true)
        { 
          _propBlock.SetFloat("_DissolveMode", 1);
          _propBlock.SetFloat("_DissolveAmount", dissolveAmout);
          _propBlock.SetFloat("_DissolveGrain", dissolveGrain);
          _propBlock.SetColor("_DissolveEmission", DissolveEmission);
          //_propBlock.SetFloat("_OpaqueMode", 1);
          _propBlock.SetColor("_OpaqueColor", DissolveColor);
        }
        else
        { 
            _propBlock.SetFloat("_DissolveMode", 0);
            //_propBlock.SetFloat("_OpaqueMode", 0); 
        }

        if (isOutline == true)
        {
           _propBlock.SetFloat("_OutlineMode", 1);
           _propBlock.SetColor("_ColorOutline", OutlineColor);
        }
        else
        { _propBlock.SetFloat("_OutlineMode", 0); }
        // Apply the edited values to the renderer.
        _renderer.SetPropertyBlock(_propBlock);

        

    }

    IEnumerator HitTime()
    {
        _propBlock.SetFloat("_OpaqueMode", 1); 
        if (Sinus <= 0.0f) {_propBlock.SetColor("_OpaqueColor", HitColor);} 
        else {_propBlock.SetColor("_OpaqueColor", HitColorTransition);}

        yield return new WaitForSeconds(0.05f);
        _propBlock.SetFloat("_OpaqueMode", 0); 
        isOpaque = false;
        
    }
}