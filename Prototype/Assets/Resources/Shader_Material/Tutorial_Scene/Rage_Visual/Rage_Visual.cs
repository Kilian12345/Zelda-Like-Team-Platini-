using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rage_Visual_Enum
{
    Normal = 0,
    Red = 1,
    two = 2,
    three = 3,
    four = 4,
    five = 5,
    six = 6,
    seven = 7,
}


[ExecuteInEditMode]
public class Rage_Visual : MonoBehaviour
{
    public Rage_Visual_Enum mode = Rage_Visual_Enum.Normal;
    private Rage_Visual_Enum previousMode = Rage_Visual_Enum.Normal;
    [SerializeField] Material _material;

    static float _baseTexture, _modifyTexture;


    #region Different Color Pattern

    private static float[,] RGB =
    {
        { _baseTexture,   _baseTexture, _baseTexture },    // Normal
        { _modifyTexture, _baseTexture, _baseTexture },    // RED
        { _baseTexture, _modifyTexture, _baseTexture },
        { _baseTexture, _baseTexture, _modifyTexture },
        { _baseTexture, _modifyTexture, _modifyTexture },
        { _modifyTexture, _modifyTexture, _baseTexture },
        { _modifyTexture, _baseTexture, _modifyTexture },
        { _modifyTexture, _modifyTexture, _modifyTexture },
    };


    #endregion

    void Awake()
    {
        _baseTexture = _material.GetFloat("_BaseTexture");
        _modifyTexture = _material.GetFloat("_ModifyTexture");

        _material.SetFloat("_R", RGB[0, 0]);
        _material.SetFloat("_G", RGB[0, 1]);
        _material.SetFloat("_B", RGB[0, 2]);
    }

    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {        
        if (mode == Rage_Visual_Enum.Normal)
        {
            Debug.Log("Kouillas DE FOU");
            Graphics.Blit(source, destination);
            return;
        }

        // Change effect
        if (mode != previousMode)
        {   
            _material.SetFloat("_BaseTexture", _baseTexture);
            _material.SetFloat("_ModifyTexture", _modifyTexture);
            _material.SetFloat("_R", RGB[(int)mode, 0]);
            _material.SetFloat("_G", RGB[(int)mode, 1]);
            _material.SetFloat("_B", RGB[(int)mode, 2]);
            previousMode = mode;
            Debug.Log("Kouillas a papa");
        }

        Debug.Log("JUSTE KOUILLAS");
        Graphics.Blit(source, destination, _material);
    }
}
