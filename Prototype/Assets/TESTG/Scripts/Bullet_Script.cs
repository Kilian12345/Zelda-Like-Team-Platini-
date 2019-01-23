using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
    [SerializeField]
    float speed = 7;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Bullet();
    }

    void Bullet()
    {
        if (speed != 0)
        {
            transform.position += transform.up * (speed * Time.deltaTime);

        }



    }

    }
