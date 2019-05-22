using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_FadeOut : MonoBehaviour
{
    SpriteRenderer spriteRend;
    Rendering_Chara renderC;
    FeedBack_Manager Fb;
    public Color color;

    bool dash, slowMo;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        Fb = FindObjectOfType<FeedBack_Manager>();
        renderC = GetComponent<Rendering_Chara>();

        renderC.HitColor = Fb.opaqueColor;
        spriteRend.material = Fb.playerMat;

               
        if (Fb.firstActivated == true) {dash = true;}
        else if (Fb.secondActivated == true) {slowMo = true;}

    }

    // Update is called once per frame
    void FixedUpdate()
    {       
        if (slowMo == true) PlayerSlowMo();
        else {PlayerDash();}

        if (spriteRend.color.a <= 0)
        {Destroy(gameObject);}

    }

    void PlayerDash()
    {    
        color = spriteRend.color;
        color.a -= Fb.ghostFadeSpeedFirst * 2f; 
        color.g -= Fb.ghostFadeSpeedFirst * 2f; 
        spriteRend.color = color;
    }

        void PlayerSlowMo()
    {
        color = spriteRend.color;
        color.a -= Fb.ghostFadeSpeedSecond * 2f; 
        spriteRend.color = color;
    }
}
