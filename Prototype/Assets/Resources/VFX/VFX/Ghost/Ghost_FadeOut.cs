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

        spriteRend.color = Fb.firstGhostColor;
        spriteRend.material = Fb.playerMat;
        renderC.OpaqueColor = new Color (1,1,1);
    }

    // Update is called once per frame
    void Update()
    {
        renderC.isOpaque = true;
        color = renderC.OpaqueColor;
        color.a -= Fb.ghostFadeSpeed * 3f; 
        renderC.OpaqueColor = color;
    }
}
