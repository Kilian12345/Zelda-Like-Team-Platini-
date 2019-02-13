using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
    ParticleSystem shadow;

    // Start is called before the first frame update
    void Start()
    {
        shadow = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        shadow.startRotation = -transform.rotation.eulerAngles.z / (180.0f / Mathf.PI);
#pragma warning restore CS0618 // Type or member is obsolete
    }


    }
