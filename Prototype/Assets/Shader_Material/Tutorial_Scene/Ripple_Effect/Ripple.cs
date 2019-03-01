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

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.Amount = this.MaxAmount;
            Vector3 pos = camera.WorldToScreenPoint(player.position); /////////////////////////////////////////// WARNING
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
