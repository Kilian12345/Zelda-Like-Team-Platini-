using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropChance : MonoBehaviour
{
    public GameObject CalciumPack;
    Player ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnDestroy()
    {

        if (Random.Range(0, 100) > ps.curDropChanceRate)
        {
            ps.curDropChanceRate += 5;
        }
        else
        {
            Instantiate(CalciumPack, transform.position, Quaternion.identity);
            ps.curDropChanceRate = ps.DropChanceRate;
        }
        
    }
}
