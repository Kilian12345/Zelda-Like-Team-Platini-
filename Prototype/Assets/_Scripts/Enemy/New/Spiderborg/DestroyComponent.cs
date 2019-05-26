using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyComponent : MonoBehaviour
{

    [SerializeField] Object obj;
    Player plScript;
    public bool striking;
    public bool isInStrike;
    public float damageValue;

    void Start()
    {
        plScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(AttackTime());
    }

    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(1.4f);
        striking = true;
        StartCoroutine(Damage());
        yield return new WaitForSeconds(0.6f);
        Destroy(obj);
        yield return null;
        //StopCoroutine(AttackTime());
    }

    IEnumerator Damage()
    {
        if(striking && isInStrike)
        {
            striking = false;
            plScript.TakeDamage(damageValue);
            Debug.Log("dAMAGE" + damageValue);
            yield return new WaitForSeconds(0.05f);
            StopCoroutine(Damage());
        }
    }
}
