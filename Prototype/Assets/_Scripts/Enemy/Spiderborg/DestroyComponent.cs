using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeZoneComponent : MonoBehaviour
{
    [SerializeField]
    CircleCollider2D col;

    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        Invoke("DisableComponent", 0.5f);
    }

    void DisableComponent()
    {
        col.enabled = false;
    }
}
