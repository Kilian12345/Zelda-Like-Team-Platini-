using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    List<GameObject> trailParts = new List<GameObject>();
    Player playerScript;
    Vector3 trailPartLocalScale;

    bool flip = false;

    [SerializeField]
    float repeat = 1;

    void Start()
    {
        InvokeRepeating("SpawnTrailPart", 0, repeat); // replace 0.2f with needed repeatRate
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

        Debug.Log(flip);


    }

    void SpawnTrailPart()
    {
        GameObject trailPart = new GameObject();
        SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
        trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
        trailPart.transform.position = transform.position;

        trailParts.Add(trailPart);

        StartCoroutine(FadeTrailPart(trailPartRenderer));
        StartCoroutine(DestroyTrailPart(trailPart, 0.5f)); // replace 0.5f with needed lifeTime
    }

    IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
    {
        Color color = trailPartRenderer.color;
        color.a -= 0.5f; // replace 0.5f with needed alpha decrement
        trailPartRenderer.color = color;

        yield return new WaitForEndOfFrame();
    }

    IEnumerator DestroyTrailPart(GameObject trailPart, float delay)
    {
        yield return new WaitForSeconds(delay);

        trailParts.Remove(trailPart);
        Destroy(trailPart);
    }

   /* void Flip()
    {
        //facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        FlipTrail();
    }*/

    void FlipTrail()
    {
        foreach (GameObject trailPart in trailParts)
        {
            trailPartLocalScale = trailPart.transform.localScale;
            trailPartLocalScale.x *= -1;
            trailPart.transform.localScale = trailPartLocalScale;
        }
    }
}
