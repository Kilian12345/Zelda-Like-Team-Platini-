using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WAVE_SYSTEM : MonoBehaviour
{
    [SerializeField] Transform objectParent;
    [SerializeField] Transform ennemyParent;
    [SerializeField] Transform[] points;
    [SerializeField] GameObject[] ennemyPrefab;
    [SerializeField] GameObject[] throwableObjects;
    [SerializeField] List<Transform> ennemiesInArena = new List<Transform>();
    public bool everyoneHasSpawn;
    bool spawnMob;
    bool spawnObj;

    void FixedUpdate()
    {
        if (everyoneHasSpawn==false)
        {

            for (int i = 0; i < points.Length; i++)
            {
                float spawn = Random.Range(0,2);
                GameObject ennemyGO;

                if (spawn == 0)
                {
                    ennemyGO = Instantiate(ennemyPrefab[Random.Range(0, ennemyPrefab.Length)], points[i].position, Quaternion.identity, ennemyParent);
                    ennemiesInArena.Add(ennemyGO.transform);
                }
                else
                {Instantiate(throwableObjects[Random.Range(0, throwableObjects.Length)], points[i].position, Quaternion.identity, objectParent);}
            }
            
            everyoneHasSpawn = true;
        }

        if (ennemiesInArena.Contains(null))
        {
            ennemiesInArena.Remove(null);
        }
        else if (ennemiesInArena.Count == 0)
        {
            everyoneHasSpawn = false;
        }

    }
}
