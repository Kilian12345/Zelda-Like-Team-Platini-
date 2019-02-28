using UnityEngine;

public class Ripple : MonoBehaviour
{
    public Transform player;
    Camera camera;
    public Material RippleMaterial;
    public float MaxAmount = 50f;

    [Range(0, 1)]
    public float Friction = .9f;

    private float Amount = 0f;
    public Vector3 pos;

    void Update()
    {
        //pos.x = player.position.x;
       // pos.y = player.position.y ;

        if (Input.GetMouseButton(0))
        {
            this.Amount = this.MaxAmount;
            Vector3 pos = Camera.main.WorldToScreenPoint(player.position); /////////////////////////////////////////// WARNING
            this.RippleMaterial.SetFloat("_CenterX", pos.x);
            this.RippleMaterial.SetFloat("_CenterY", pos.y);
            Debug.Log (pos.x);
            Debug.Log (pos.y);
        }

        this.RippleMaterial.SetFloat("_Amount", this.Amount);
        this.Amount *= this.Friction;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, this.RippleMaterial);
    }
}
