using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    List<GameObject> trailParts = new List<GameObject>();
    Player playerScript;
    Vector3 trailPartLocalScale;
    GameObject trailPart;

    bool flip = false;

    [SerializeField]
    GameObject parent;

    public float repeat = 1;
    public float lifetime = 0.5f;
    public float alpha = 0.5f;

    void Start()
    {
        InvokeRepeating("SpawnTrailPart", 0, repeat); 
        playerScript = GetComponent<Player>();

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


        if (flip == true)
        {
            FlipTrail();
        }

        Debug.Log(trailParts);



    }

    void SpawnTrailPart()
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

    IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
    {
        Color color = trailPartRenderer.color;
        color.a -= alpha;
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
