using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyComponent : MonoBehaviour
{

    [SerializeField] Object obj;
    Player plScript;
    bool striking = false;
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
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(Damage());
        }
    }

    IEnumerator Damage()
    {
        if(striking == true)
        {
            striking = false;
            plScript.TakeDamage(damageValue);
            yield return new WaitForSeconds(0.01f);
            StopCoroutine(Damage());
        }
    }
}
