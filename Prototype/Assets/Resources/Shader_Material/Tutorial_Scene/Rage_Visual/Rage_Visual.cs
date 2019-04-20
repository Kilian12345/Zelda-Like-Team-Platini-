using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public enum Rage_Visual_Enum
{
    Normal = 0,
    Red = 1,
    Green = 2,
    Blue = 3,
    Red_Green = 4,
    Blue_Green = 5,
    Red_Blue = 6,
}*/


[ExecuteInEditMode]
public class Rage_Visual : MonoBehaviour
{
    public Rage_Visual_Enum mode = Rage_Visual_Enum.Normal;
    private Rage_Visual_Enum previousMode = Rage_Visual_Enum.Normal;
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

        _material.SetFloat("_Which", RGB[0,0]);
    }

    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {        
        if (mode == Rage_Visual_Enum.Normal)
        {
            Graphics.Blit(source, destination);
            return;
        }

        // Change effect
        if (mode != previousMode)
        {   
            _material.SetFloat("_Which", RGB[(int)mode, 0]);
            previousMode = mode;
        }

        _material.SetFloat("_OffsetColor", offsetColor);
        offsetColor = Fb_Mana.offsetColor;

        Graphics.Blit(source, destination, _material);
    }
}
