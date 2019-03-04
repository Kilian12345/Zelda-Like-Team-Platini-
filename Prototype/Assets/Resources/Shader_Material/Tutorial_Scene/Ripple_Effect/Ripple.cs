using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple : MonoBehaviour
{
    FeedbacksOrder Fb_Order;

    public Transform player;
    Camera camera;
    public Material RippleMaterial;
    public float MaxAmount = 50f;
    public bool canripple;

    public float secondsToWait = 0.2f;

    [Range(0, 1)]
    public float Friction = .9f;

    private float Amount = 0f;
    public Vector3 pos;

    private void Start()
    {
        camera = GetComponent<Camera>();
        Fb_Order = FindObjectOfType<FeedbacksOrder>();//////////////////////////////////sale
    }
    
    void Update()
    {
        RippleEffect();

    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, this.RippleMaterial);
    }

    IEnumerator rippleTime()
    {
        this.Amount = this.MaxAmount;
        Vector3 pos = camera.WorldToScreenPoint(player.position);
        this.RippleMaterial.SetFloat("_CenterX", pos.x);
        this.RippleMaterial.SetFloat("_CenterY", pos.y);
        yield return new WaitForSeconds(secondsToWait);
        canripple = false;
    }

    void RippleEffect()
    {
        if (Fb_Order.ripple == true && canripple == true)
        {
            StartCoroutine(rippleTime());
        }
        else if (Fb_Order.ripple == false)
        {
            canripple = true;
        }

        this.RippleMaterial.SetFloat("_Amount", this.Amount);
        this.Amount *= this.Friction;
    }
}
