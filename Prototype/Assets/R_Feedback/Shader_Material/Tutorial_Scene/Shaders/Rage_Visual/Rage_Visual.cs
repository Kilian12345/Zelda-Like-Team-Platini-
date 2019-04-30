using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rage_Visual_Enum
{
    Normal = 0,
    Red = 1,
    Green = 2,
    Blue = 3,
    Red_Green = 4,
    Blue_Green = 5,
    Red_Blue = 6,
}


[ExecuteInEditMode]
public class Rage_Visual : MonoBehaviour
{
    public Rage_Visual_Enum mode;
    private Rage_Visual_Enum previousMode;
    [SerializeField] Material _material;
    FeedBack_Manager Fb_Mana;

    float offsetColor;


    #region Different Color Pattern

    private static float[,] RGB =
    {
        { 0 },    // Normal
        { 1 },    // RED
        { 2 },
        { 3 },
        { 4 },
        { 5 },
        { 6 },
    };


    #endregion

    void Awake()
    {
        Fb_Mana = GetComponentInParent<FeedBack_Manager>();

    }

    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        mode = Fb_Mana.mode;
        previousMode = Fb_Mana.previousMode;
        offsetColor = Fb_Mana.offsetColor;

        if (Fb_Mana.mode == Rage_Visual_Enum.Normal)
        {
            Graphics.Blit(source, destination);
            return;
        }

        // Change effect
        if (Fb_Mana.mode != Fb_Mana.previousMode)
        {   
            _material.SetFloat("_Which", RGB[(int)mode, 0]);
            Fb_Mana.previousMode = Fb_Mana.mode;
        }

        // Glitch Effect
        if (Fb_Mana.glitchEffect == true)
        {
            if (Fb_Mana.colorGlitch < -0.001f)
            {
                Fb_Mana.colorGlitch += Fb_Mana.glitchSpeed;
            }
            else if (Fb_Mana.colorGlitch > 0.001f)
            {
                Fb_Mana.colorGlitch -= Fb_Mana.glitchSpeed;
            }
            else if (Fb_Mana.colorGlitch >= -0.001f && Fb_Mana.colorGlitch <= 0.001f)
            {
                Fb_Mana.colorGlitch = Random.Range(-Fb_Mana.glitchPower, Fb_Mana.glitchPower);
            }

            if (Fb_Mana.colorSwitch == true)
            {
                if (Fb_Mana.colorActual > 0)
                {
                    Fb_Mana.colorActual -= Fb_Mana.colorSwitchSpeed;
                }
                else
                {
                    Fb_Mana.colorActual = Random.Range(0, Fb_Mana.colorMaxTime);
                    _material.SetFloat("_Which", Random.Range(RGB[1,0], RGB[3, 0]));
                }

            }

            _material.SetFloat("_OffsetColor", Fb_Mana.colorGlitch);
        }
        else
        {
            _material.SetFloat("_OffsetColor", offsetColor);
        }

        Debug.Log(Fb_Mana.colorGlitch);

        Graphics.Blit(source, destination, _material);
    }
}
