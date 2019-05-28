using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixDialogueScene3 : MonoBehaviour
{
    [SerializeField] Animator anim;

    void Start()
    {
        anim.SetInteger("AnimChara", 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
