using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    FeedbacksOrder Fb_Order;

    List<GameObject> trailParts = new List<GameObject>();
    Player playerScript;
    Vector3 trailPartLocalScale;
    GameObject trailPart;

    bool flip = false;
    float time = 0f;

    [SerializeField]
    GameObject parent;

    public float repeat = 1;
    public float lifetime = 20;
    public float alpha = 0.5f;

    void Start()
    {
        Fb_Order = FindObjectOfType<FeedbacksOrder>();
        InvokeRepeating("SpawnTrailPart", 0, repeat); 
        playerScript = GetComponent<Player>();

    }

    void Update()
    {

        Debug.Log(trailParts.IndexOf(trailPart));

        if (playerScript.moveHor < 0f)
        {
            flip = true;
        }
        else
        {
            flip = false;
        }


        if (flip == true)
        {
            FlipTrail();
        }




    }

    void SpawnTrailPart()
    {
        if ( Fb_Order.valueList == 1)
        {

            trailPart = new GameObject();
            SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
            trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
            trailPart.transform.position = transform.position;
            trailPart.transform.localScale = transform.localScale;
            trailParts.Add(trailPart);

            trailPart.transform.parent = parent.transform;
            trailPart.layer = LayerMask.NameToLayer("Player");

            StartCoroutine(FadeTrailPart(trailPartRenderer));

            Destroy(trailPart, lifetime);
        }
        
    }

    IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
    {

        Color color = trailPartRenderer.color;
        color.a = alpha ;

        color.r = 0;
        color.g = 0;
        color.b = 0;

        time += Time.deltaTime / lifetime;
        color.a = Mathf.Lerp(color.a, 1, time);
        color.r = Mathf.Lerp(color.r, 0, time);
        color.g = Mathf.Lerp(color.g, 0, time);
        color.b = Mathf.Lerp(color.b, 0, time);

        trailPartRenderer.color = color;

    



        yield return new WaitForEndOfFrame();
    }

    IEnumerator DestroyTrailPart(GameObject trailPart, float delay)
    {
        yield return new WaitForSeconds(delay);

        trailParts.Remove(trailPart);
        Destroy(trailPart);
    }


    void FlipTrail()
    {
        foreach (GameObject trailPart in trailParts)
        {
            trailPartLocalScale = trailPart.transform.localScale;
            trailPartLocalScale.x = -trailPartLocalScale.x;
            trailPart.transform.localScale = trailPartLocalScale;
        }
    }
}
