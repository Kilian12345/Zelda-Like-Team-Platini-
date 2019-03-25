using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeSingletonClass : MonoBehaviour
{
    public static SomeSingletonClass managerInstance;

    void Awake()
    {
        if (managerInstance)
        {
            Destroy(this);
            return;
        }

        managerInstance = this;
    }
}
