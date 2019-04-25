using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GrayScale : MonoBehaviour
{

    FeedBack_Manager Fb_Mana;

#region Variables
    public Shader curShader;
public float brightnessAmount_;
    public float saturationAmount_;
public float contrastAmount_;
public float strength_;
private Material curMaterial;

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

        Fb_Mana = GetComponentInParent<FeedBack_Manager>();

    }

void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
{
    if (curShader != null)
    {

        material.SetFloat("_BrightnessAmount", brightnessAmount_);
        material.SetFloat("_SaturationAmount", saturationAmount_);
        material.SetFloat("_ContrastAmount", contrastAmount_);

        material.SetFloat("_Strength", strength_);
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
        brightnessAmount_ = Fb_Mana.brightnessAmount;
        saturationAmount_ = Fb_Mana.saturationAmount;
        contrastAmount_ = Fb_Mana.contrastAmount;
        strength_ = Fb_Mana.strength;


    }

void OnDisable()
{
    if (curMaterial)
    {
        DestroyImmediate(curMaterial);
    }

}
	
	
}