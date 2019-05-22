using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WAVE_SYSTEM : MonoBehaviour
{
    [SerializeField] Vector3[] points;
    [SerializeField] GameObject[] ennemyPrefab;
    [SerializeField] float ennemiesInArena;
    bool everyoneHasSpawn;

    void FixedUpdate()
    {
        if (ennemiesInArena == 0)
        {
            for (int i = 0; i < ennemyPrefab.Length; i++)
            {
                
            }
        }
    }
}
