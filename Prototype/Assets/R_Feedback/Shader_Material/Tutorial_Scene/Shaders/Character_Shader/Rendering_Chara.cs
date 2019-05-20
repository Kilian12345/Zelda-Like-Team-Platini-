using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rendering_Chara : MonoBehaviour
{
    public Color Tint = new Color(1,1,1,1)
        , HitColor = new Color(1,1,1,1)
        , DissolveColor = new Color(0,0,0,1)
        , DissolveEmission = new Color(1, 1, 1, 1)
        , OutlineColor = new Color(1, 1, 1, 1);
    Color WhiteHit = Color.white;
    Color FeedBack_Hit;
    Color HitColorTransition = Color.black;
    [HideInInspector] public Color transitionDissolve;

    public bool isOpaque, isDissolve, isOutline, isPlayer;
    [Range(40 , 80)] public float timeHitFB;
    [Range(0 , 0.5f)] public float hitDuration = 0.05f;
    double Sinus;

    [Range (-0.5f,1.1f)]public float dissolveAmout = -0.05f;
    [Range(0 , 0.5f)] public float dissolveGrain = 0.4f;
    private Renderer _renderer;
    private Player plScript;
    private MaterialPropertyBlock _propBlock;


    void Awake()
    {
        plScript = FindObjectOfType<Player>();
        _propBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();        
    }

    void LateUpdate()
    {

        _renderer.GetPropertyBlock(_propBlock);
        // Assign our new value.
        _propBlock.SetColor("_Color", Tint * 1.3f);

         if (isOpaque == true)
         {Sinus = Mathf.Sin(Time.time*timeHitFB) * 1.2; StartCoroutine(HitTime());}
         else if (plScript.takeDamage == true && isPlayer == true)
         {Sinus = Mathf.Sin(Time.time*timeHitFB) * 1.2; StartCoroutine(HitTime());}
         else
         { _propBlock.SetFloat("_OpaqueMode", 0); }



        if (isDissolve == true)
        { 
            Dissolve();
        }
        else
        { 
            _propBlock.SetFloat("_DissolveMode", 0);
            //_propBlock.SetFloat("_OpaqueMode", 0);            
            transitionDissolve = Tint;

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
        isOpaque = true;
        _propBlock.SetFloat("_OpaqueMode", 1); 
        if (plScript.damage <= 45 && isPlayer == false) {FeedBack_Hit = WhiteHit;}
        else {FeedBack_Hit = HitColor;}

        if (Sinus <= 0.0f) {_propBlock.SetColor("_OpaqueColor", FeedBack_Hit);} 
        else {_propBlock.SetColor("_OpaqueColor", HitColorTransition);}

        yield return new WaitForSeconds(hitDuration);
        _propBlock.SetFloat("_OpaqueMode", 0); 
        isOpaque = false;
        plScript.takeDamage = false;

        
    }

    void Dissolve() //// When the nigga died
    {
        if(transitionDissolve != Color.black)
        {
            transitionDissolve = new Color
            (transitionDissolve.r - 0.033f,
             transitionDissolve.g - 0.033f,
             transitionDissolve.b - 0.033f);
        }
        
        Tint = transitionDissolve;

        if (transitionDissolve.r <= 0 && dissolveAmout < 1.1f)
        {
          _propBlock.SetFloat("_DissolveMode", 1);
          _propBlock.SetFloat("_DissolveAmount", dissolveAmout);
          _propBlock.SetFloat("_DissolveGrain", dissolveGrain);
          _propBlock.SetColor("_DissolveEmission", DissolveEmission);
          _propBlock.SetFloat("_OpaqueMode", 1);
          _propBlock.SetColor("_OpaqueColor", DissolveColor);

            dissolveAmout += 0.02f;

        }
        else if (dissolveAmout >= 1.1f)
        {
            //dissolveAmout = -0.5f;
            isDissolve = false;
            //Tint = Color.white;
        }

    }
}
