using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    public float timeOfDestruction;
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, timeOfDestruction);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
