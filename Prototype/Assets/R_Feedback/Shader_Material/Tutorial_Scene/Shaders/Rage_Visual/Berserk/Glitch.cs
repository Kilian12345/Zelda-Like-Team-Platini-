using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitch : MonoBehaviour
{
    [SerializeField] Material _material;
    FeedBack_Manager Fb;

    void Start()
    {
        Fb = GetComponentInParent<FeedBack_Manager>();
    }
public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (Fb.glitchRageEnabled == true) {Graphics.Blit(source, destination, _material);}
        else {Graphics.Blit(source, destination);}
    }
}
