using UnityEngine;

public class Ripple : MonoBehaviour
{
    FeedbacksOrder Fb_Order;
    public Transform player;
    Camera camera;
    public Material RippleMaterial;
    public float MaxAmount = 50f;
    public bool ripple = false;

    [Range(0, 1)]
    public float Friction = .9f;

    private float Amount = 0f;
    public Vector3 pos;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {

        if (ripple == true)
        {
            this.Amount = this.MaxAmount;
            Vector3 pos = camera.WorldToScreenPoint(player.position); 
            this.RippleMaterial.SetFloat("_CenterX", pos.x);
            this.RippleMaterial.SetFloat("_CenterY", pos.y);

        }

        this.RippleMaterial.SetFloat("_Amount", this.Amount);
        this.Amount *= this.Friction;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, this.RippleMaterial);
    }
}
