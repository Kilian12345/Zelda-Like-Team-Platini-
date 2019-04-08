using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject trigger;
    public float yScale;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (trigger.GetComponent<Trigger>().isTriggered)
        {
            if (transform.localScale.y != 0)
            {
                StartCoroutine(Open());
            }
        }
        else
        {
            if (transform.localScale.y <= yScale)
            {
                StartCoroutine(Close());
            }
        }
    }

    IEnumerator Open()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y*0.99f, transform.localScale.z);
        yield return new WaitForSeconds(0.1f);
        StopCoroutine(Open());
    }

    IEnumerator Close()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 1.01f, transform.localScale.z);
        yield return new WaitForSeconds(0.1f);
        StopCoroutine(Close());
    }
}
