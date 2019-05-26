using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{

    public float DestroyTime = 3f;
    //public Vector3 Offset = new Vector3(0, 2, 0);
    public Vector3 RandomizeIntensity = new Vector3(0.01f, 0, 0);

    void Start()
    {
        //transform.localPosition += Offset;
        Destroy(gameObject, DestroyTime);
        transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
        Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
        Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z));
    }

    private void Update()
    {
        //transform.localScale = new Vector3(transform.localScale.x + 0.01f, transform.localScale.y + 0.00f, transform.localScale.z + 0.01f);
    }

}
