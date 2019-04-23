using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_FadeOut : MonoBehaviour
{
    SpriteRenderer spriteRend;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Color color = spriteRend.color;
        color.a -= 0.05f;
        spriteRend.color = color;
    }
}
