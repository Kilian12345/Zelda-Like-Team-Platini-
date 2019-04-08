using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    public float timeOfDestruction;

    void Start()
    {
        Destroy(gameObject, timeOfDestruction);
    }
}
