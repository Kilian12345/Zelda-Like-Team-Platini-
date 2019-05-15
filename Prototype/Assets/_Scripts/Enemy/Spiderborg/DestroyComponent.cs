using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyComponent : MonoBehaviour
{

    [SerializeField]
    Object obj;


    void Start()
    {
        StartCoroutine(AttackTime());
    }

    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(2.4f);
        Destroy(obj);
        yield return null;
    }

}
