using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple : MonoBehaviour
{
    [SerializeField] FeedBack_Manager Fb_Mana;

    public Transform player;
    Camera camera;
    public Material RippleMaterial;
    public float MaxAmount = 50f;
    public bool canripple;


    public Vector3 pos;

    private void Start()
    {

    }
    
    void Update()
    {
        camera = Fb_Mana.camera;
        MaxAmount = Fb_Mana.MaxAmount;
        player = Fb_Mana.target;
        RippleEffect();


    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, this.RippleMaterial);
    }

    IEnumerator rippleTime()
    {
        this.Fb_Mana.Amount = this.Fb_Mana.MaxAmount;
        Vector3 pos = camera.WorldToScreenPoint(player.position);
        this.RippleMaterial.SetFloat("_CenterX", pos.x);
        this.RippleMaterial.SetFloat("_CenterY", pos.y);
        yield return new WaitForSeconds(Fb_Mana.secondsToWait);
        canripple = false;
    }

    void RippleEffect()
    {
        if (Fb_Mana.ripple == true && canripple == true)
        {
            StartCoroutine(rippleTime());
        }
        else if (Fb_Mana.ripple == false)
        {
            canripple = true;
        }

        this.RippleMaterial.SetFloat("_Amount", this.Fb_Mana.Amount);
        this.Fb_Mana.Amount *= this.Fb_Mana.Friction;
    }
}
