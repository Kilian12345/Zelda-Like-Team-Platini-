using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GrayScale : MonoBehaviour
{

    FeedbacksOrder Fb_Order;

#region Variables
    public Shader curShader;
public float brightnessAmount;
    public float saturationAmount;
public float contrastAmount;
public float strength;
private Material curMaterial;
bool grayActive = false;

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

        Fb_Order = GetComponent<FeedbacksOrder>();

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
        brightnessAmount = Fb_Order.brightness;
        saturationAmount = Fb_Order.saturation;
        contrastAmount = Fb_Order.contrast;
        strength = Fb_Order.strength;

        //////////////////////////////////////////////////////////////////////////////////////////////////////// MOVE
        if (Input.GetKeyDown(KeyCode.Space) && grayActive == false)
        {
            grayActive = true;
            //saturationAmount = 0;

        }
        else if (Input.GetKeyDown(KeyCode.Space) && grayActive == true)
        {
            grayActive = false;
            //saturationAmount = 1;
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