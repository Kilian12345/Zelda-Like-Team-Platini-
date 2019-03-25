using System.Collections;
using UnityEngine;

public class Ghost2 : MonoBehaviour
{
    float time;

    void Start()
    {
        InvokeRepeating("SpawnTrailPart", 0, 0.2f); // replace 0.2f with needed repeatRate
    }

    void SpawnTrailPart()
    {
        GameObject trailPart = new GameObject();
        SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
        trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
        trailPart.transform.position = transform.position;
        trailPart.transform.localScale = transform.localScale; // We forgot about this line!!!

        StartCoroutine(FadeTrailPart(trailPartRenderer));
        Destroy(trailPart, 10); // replace 0.5f with needed lifeTime
    }

    IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
    {
        Color color = trailPartRenderer.color;
        time += Time.deltaTime * 10;
        color.a = Mathf.Lerp(color.a, 0, time ); // replace 0.5f with needed alpha decrement
        trailPartRenderer.color = color;

        yield return new WaitForEndOfFrame();
    }

    /////// attach de quoi fade sur cahque fantome crée
}
