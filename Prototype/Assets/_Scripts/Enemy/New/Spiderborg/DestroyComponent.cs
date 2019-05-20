using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyComponent : MonoBehaviour
{

    [SerializeField] Object obj;
    Player plScript;
    bool striking;
    bool dontdoshit = false;
    public float damageValue = 50;

    void Start()
    {
        plScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(AttackTime());
    }

    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(1.4f);
        striking = true;
        yield return new WaitForSeconds(0.6f);
        Destroy(obj);
        yield return null;
        //StopCoroutine(AttackTime());
    }

     private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != "Player")
        {
            dontdoshit = true;
            StopCoroutine(Damage());
        }
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(Damage());
        }
    }

     /* private void OnTriggerStay2D(Collider2D col)
    {
         if (col.gameObject.tag != "Player")
        {
            StopCoroutine(Damage());
        }
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(Damage());
        }
    }*/
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag != "Player")
        {
            StartCoroutine(Damage());
        }
        if (col.gameObject.tag == "Player")
        {
            dontdoshit = true;
            StopCoroutine(Damage());
        }
    }

    IEnumerator Damage()
    {
        if(striking == true)
        {
            striking = false;
            if(dontdoshit == false)
            {
            plScript.TakeDamage(damageValue);
            }

            yield return new WaitForSeconds(0.05f);
            StopCoroutine(Damage());
        }
    }
}
