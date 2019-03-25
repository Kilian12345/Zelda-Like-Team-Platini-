using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SimpleBlit : MonoBehaviour
{
    FeedBack_Manager Fb_Mana;
    public Material TransitionMaterial;

    float time;
    bool done;

    private void Start()
    {
        Fb_Mana = GetComponentInParent<FeedBack_Manager>();

        TransitionMaterial.shader = Shader.Find("Custom/BattleTransitions");

    }

    private void Update()
    {
        Cutoff();
    }

    void Cutoff()
    {
        TransitionMaterial = Fb_Mana.TransitionMaterial;

        float shininess = Mathf.Lerp(0, Fb_Mana.cutoff, time);
        TransitionMaterial.SetFloat("_Cutoff", shininess);


        if (Input.GetKey(KeyCode.Space))
        {

            time += Time.deltaTime / Fb_Mana.transitionTime;
            done = false;

        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            done = true;
        }

        if (done)
        {
            time = Mathf.Lerp(time, 0, Time.deltaTime / time);
        }

        if (time < 0.05f && done == true)
        {
            time = 0;
        }

        if (time > Fb_Mana.cutoff) time = Fb_Mana.cutoff;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {

        if (TransitionMaterial != null)
            Graphics.Blit(src, dst, TransitionMaterial);
    }
}
