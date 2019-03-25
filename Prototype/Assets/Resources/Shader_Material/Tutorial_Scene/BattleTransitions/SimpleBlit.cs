using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SimpleBlit : MonoBehaviour
{
    FeedBack_Manager Fb_Mana;
    public Material TransitionMaterial;

    private void Start()
    {
        Fb_Mana = GetComponentInParent<FeedBack_Manager>();

        TransitionMaterial.shader = Shader.Find("Fade");

    }

    private void Update()
    {
        TransitionMaterial = Fb_Mana.TransitionMaterial;

       /* if (Input.GetKey(KeyCode.Space))
        {
            TransitionMaterial.SetFloat("_Cutoff", Mathf.Lerp(0, Fb_Mana.cutoff, Time.deltaTime * 0.9f));
        }*/
        ///////////////////////////////DLA MERDE DE OUF
    }


    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {

        if (TransitionMaterial != null)
            Graphics.Blit(src, dst, TransitionMaterial);
    }
}
