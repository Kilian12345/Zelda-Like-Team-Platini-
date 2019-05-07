using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject trigger;
    public float yScale;
    [Range(0,1)]
    public float doorSpeedOpen;
    [Range(0, 1)]
    public float doorSpeedClose;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (trigger.GetComponent<Trigger>().isTriggered)
        {
            if (transform.localScale.y < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
            }
            else
            {
                if (transform.localScale.y > 0)
                {
                    StartCoroutine(Open());
                }
            }
        }
        else
        {
            if (transform.localScale.y > yScale)
            {
                transform.localScale = new Vector3(transform.localScale.x, yScale, transform.localScale.z);
            }
            else
            {
                if (transform.localScale.y < yScale)
                {
                    StartCoroutine(Close());
                }
            }
        }
    }

    IEnumerator Open()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y- doorSpeedOpen, transform.localScale.z);
        yield return new WaitForSeconds(0.1f);
        StopCoroutine(Open());
    }

    IEnumerator Close()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + doorSpeedClose, transform.localScale.z);
        yield return new WaitForSeconds(0.1f);
        StopCoroutine(Close());
    }
}
