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
    private float ghostLifetime;


    void Start()
    {
        
    }

    void LateUpdate()
    {
        if (playerScript.moveHor < 0f) {flip = true;}
        else {flip = false;}

        if (Fb.firstActivated == true)
        {
            if(firstInvokeDone == false)
            {
            ghostLifetime = Fb.ghostLifetimeFirst;
            CancelInvoke();
            InvokeRepeating("SpawnTrailPart", 0, Fb.ghostSpawnRateFirst);
            Debug.Log("1st");
            firstInvokeDone = true;
            }
        }
        else {firstInvokeDone = false;}

        if (Fb.secondActivated == true)
        {
            if(secondInvokeDone == false)
            {
            ghostLifetime = Fb.ghostLifetimeSecond;
            CancelInvoke();
            InvokeRepeating("SpawnTrailPart", 0, Fb.ghostSpawnRateSecond);
            Debug.Log("2nd");
            secondInvokeDone = true;
            }
        }
        else {secondInvokeDone = false;}
    }

    public void SpawnTrailPart()
    {
        if (Fb.ghostAcivated == true)
        {

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

        }
        
    }

}
