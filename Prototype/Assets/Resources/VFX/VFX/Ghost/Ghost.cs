using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    Ghost_FadeOut ghFadeOut;
    [SerializeField] FeedBack_Manager Fb;

    List<GameObject> trailParts = new List<GameObject>();
    [SerializeField] Player playerScript;
    Vector3 trailPartLocalScale;
    GameObject trailPart;

    bool flip = false;
    
    [SerializeField] GameObject parent;


    void Start()
    {
        
        InvokeRepeating("SpawnTrailPart", 0, Fb.ghostSpawnRate); 
        //playerScript = GetComponent<Player>();

    }

    void Update()
    {


        if (playerScript.moveHor < 0f)
        {
            flip = true;
        }
        else
        {
            flip = false;
        }

        Debug.Log(playerScript.moveHor);

    }

    void SpawnTrailPart()
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
            trailParts.Add(trailPart);
            trailPart.layer = LayerMask.NameToLayer("Default");

            Destroy(trailPart, Fb.ghostLifetime);
        }
        
    }

  /*  IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
    {

        Color color = trailPartRenderer.color;
        color.a = alpha ;

        color.r = 0;
        color.g = 0;
        color.b = 0;

        time += Time.deltaTime * 10;
        color.a = Mathf.Lerp(color.a, 0, time  );
        color.r = Mathf.Lerp(color.r, 0, time);
        color.g = Mathf.Lerp(color.g, 0, time);
        color.b = Mathf.Lerp(color.b, 0, time);

        trailPartRenderer.color = color;

    



        yield return new WaitForEndOfFrame();
    }*/

    IEnumerator DestroyTrailPart(GameObject trailPart, float delay)
    {
        yield return new WaitForSeconds(delay);

        trailParts.Remove(trailPart);
        Destroy(trailPart);
    }

}
