using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GrayScale : MonoBehaviour
{ 

#region Variables
public Shader curShader;
public float brightnessAmount = 1.0f;
public float saturationAmount = 1.0f;
public float contrastAmount = 1.0f;
public float strength = 0;
private Material curMaterial;
bool grayActive = true;
#endregion

#region Properties
Material material
{
    get
    {
        if (curMaterial == null)
        {
            curMaterial = new Material(curShader);
            curMaterial.hideFlags = HideFlags.HideAndDontSave;
        }
        return curMaterial;
    }
}
#endregion
// Use this for initialization
void Start()
{
    if (!SystemInfo.supportsImageEffects)
    {
        enabled = false;
        return;
    }
}

void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
{
    if (curShader != null)
    {
        material.SetFloat("_BrightnessAmount", brightnessAmount);
        material.SetFloat("_SaturationAmount", saturationAmount);
        material.SetFloat("_ContrastAmount", contrastAmount);
        material.SetFloat("_Strength", strength);
        Graphics.Blit(sourceTexture, destTexture, material);
    }
    else
    {
        Graphics.Blit(sourceTexture, destTexture);
    }


}

// Update is called once per frame
void Update()
{
    brightnessAmount = Mathf.Clamp(brightnessAmount, -30f, 30f);
    saturationAmount = Mathf.Clamp(saturationAmount, -30f, 30f);
    contrastAmount = Mathf.Clamp(contrastAmount, -30f, 30f);
    strength = Mathf.Clamp(strength, -1f, 1f);

        //////////////////////////////////////////////////////////////////////////////////////////////////////// MOVE
        if (Input.GetKeyDown(KeyCode.Space) && grayActive == false)
        {
            grayActive = true;
            saturationAmount = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && grayActive == true)
        {
            grayActive = false;
            saturationAmount = 1;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////
    }

void OnDisable()
{
    if (curMaterial)
    {
        DestroyImmediate(curMaterial);
    }

}
	
	
}