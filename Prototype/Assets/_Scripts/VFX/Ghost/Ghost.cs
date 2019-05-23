using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] FeedBack_Manager Fb;
    [SerializeField] GameObject parent;    
    [SerializeField] Player playerScript;

    List<GameObject> trailParts = new List<GameObject>();
    Vector3 trailPartLocalScale;
    GameObject trailPart;

    bool flip = false;
    bool firstInvokeDone = false;
    bool secondInvokeDone = false;  


    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (playerScript.moveHor < 0f) {flip = true;}
        else {flip = false;}

        if (Fb.firstActivated == true)
        {
                dashGhost();
        }
        else if (playerScript.activatedAbility != 1)
        {firstInvokeDone = false; CancelInvoke("SpawnTrailPart");}

    }

void secondAbilityPunch()
{
        if (Fb.secondActivated == true)
        {
                Fb.ghostAcivated = true;

                if(secondInvokeDone == false)
                {
                    //CancelInvoke();
                    //InvokeRepeating("SpawnTrailPart", 0, Fb.ghostSpawnRateSecond);
                    secondInvokeDone = true;
                }

        }
        else {secondInvokeDone = false; Fb.ghostAcivated = false;}
}
    public void SpawnTrailPart()
    {
            //Debug.Log("salop salop salop");
        //if (Fb.ghostAcivated == true)
       // {

            trailPart = new GameObject();
            SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
            trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
            trailPart.transform.position = transform.position;
            trailPart.transform.localScale = transform.localScale;


            trailPart.transform.parent = parent.transform;
            trailPart.layer = LayerMask.NameToLayer("Player");

            //StartCoroutine(FadeTrailPart(trailPartRenderer));
            trailPart.AddComponent<Ghost_FadeOut>();
            trailPart.AddComponent<Rendering_Chara>();
            trailParts.Add(trailPart);
            trailPart.layer = LayerMask.NameToLayer("Default");

            trailPartRenderer.sortingLayerName = "Default";
            trailPartRenderer.sortingOrder = 0;

      //  }
        
    }

    public void dashGhost()
    {
        if(firstInvokeDone == false)
        {
            InvokeRepeating("SpawnTrailPart", 0, Fb.ghostSpawnRateFirst);
            firstInvokeDone = true;
        }
    }

}
