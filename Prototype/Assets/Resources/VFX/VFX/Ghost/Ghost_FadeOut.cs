using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_FadeOut : MonoBehaviour
{
    SpriteRenderer spriteRend;
    FeedBack_Manager Fb;


    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        Fb = FindObjectOfType<FeedBack_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        Color color = spriteRend.color;
        color.a -= Fb.ghostFadeSpeed * 0.1f;
        spriteRend.color = color;
    }
}
