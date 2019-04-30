using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_FadeOut : MonoBehaviour
{
    SpriteRenderer spriteRend;
    Rendering_Chara renderC;
    FeedBack_Manager Fb;
    public Color color;


    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        Fb = FindObjectOfType<FeedBack_Manager>();
        renderC = GetComponent<Rendering_Chara>();

        renderC.OpaqueColor = Fb.opaqueColor;
        spriteRend.material = Fb.playerMat;
    }

    // Update is called once per frame
    void Update()
    {
        if (Fb.firstActivated == true) PlayerDash();
        else if (Fb.secondActivated == true)PlayerSlowMo();
    }

    void PlayerDash()
    {
        renderC.isOpaque = true;
        color = renderC.OpaqueColor;
        color.a -= Fb.ghostFadeSpeedFirst * 2f; 
        renderC.OpaqueColor = color;
    }

        void PlayerSlowMo()
    {
        renderC.isOpaque = false;
        color = spriteRend.color;
        color.a -= Fb.ghostFadeSpeedSecond * 2f; 
        spriteRend.color = color;
    }
}
