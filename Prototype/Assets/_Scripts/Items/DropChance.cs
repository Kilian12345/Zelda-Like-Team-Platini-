using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropChance : MonoBehaviour
{
    public GameObject CalciumPack;
    Player ps;
    GameObject player;
    private bool canSpawn;
    public bool isDestroy = false;
    EnemyHealth healthScript;
    ThrowingMechanic throwMecha;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ps = player.GetComponent<Player>();

         if (GetComponent<EnemyHealth>() != null)
             {healthScript= GetComponent<EnemyHealth>();}

         if (GetComponent<ThrowingMechanic>() != null)
                {throwMecha= GetComponent<ThrowingMechanic>();}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetComponent<EnemyHealth>() != null)
        {
            if (!ps.isDead && (healthScript.health<=0))
            {canSpawn = true;}
            else
            {canSpawn = false;}
        }

        if (GetComponent<ThrowingMechanic>() != null)
        {
            if (!ps.isDead && (isDestroy == true))
            {canSpawn = true;}
            else
            {canSpawn = false;}

        }

    }

    /*void OnDestroy()
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
        
    }*/

    private void OnDisable()
    {
        if (canSpawn)
        {
            if (Random.Range(0, 100) > ps.curDropChanceRate && (GetComponent<EnemyHealth>() != null))
            {
                ps.curDropChanceRate += 2f;
            }
            else if (Random.Range(0, 300) > ps.curDropChanceRate && (GetComponent<ThrowingMechanic>() != null))
            {
                ps.curDropChanceRate += 2f;
            }
            else
            {
                Instantiate(CalciumPack, transform.position, Quaternion.identity);
                ps.curDropChanceRate = ps.DropChanceRate;
            }
        }
        
    }
}
