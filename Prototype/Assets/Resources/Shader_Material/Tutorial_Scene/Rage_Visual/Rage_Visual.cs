using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Rage_Visual : MonoBehaviour
{
    [SerializeField] Material _material;
    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        Graphics.Blit(source, destination, _material);
    }
}
